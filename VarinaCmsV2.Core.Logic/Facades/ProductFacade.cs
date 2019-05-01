using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.Builders;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.LogicFacade;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Core.Logic.Facades
{
    public class ProductFacade : IProductFacade
    {
        private readonly List<IBaseBeforeUpdatingEntityLogic> _baseBeforeUpdateEntityLogics;
        private readonly IEntityBuilder<Product, ProductCategory> _builder;
        private readonly IDiscountDataService _discounts;
        private readonly IFileDataDataService _files;
        private readonly IIdentityManager _identityManager;
        private readonly IProductCategoryDataService _productCategories;
        private readonly IProductDataService _products;
        private readonly IEntitySchemeDataService _schemes;
        private readonly IUnitOfWork _unitOfWork;

        public ProductFacade(IEntityBuilder<Product, ProductCategory> builder, IEntitySchemeDataService schemes,
            IProductCategoryDataService productCategories, IProductDataService products, IDiscountDataService discounts,
            IUnitOfWork unitOfWork, IFileDataDataService files,
            List<IBaseBeforeUpdatingEntityLogic> baseBeforeUpdateEntityLogics, IIdentityManager identityManager)
        {
            _builder = builder;
            _schemes = schemes;
            _productCategories = productCategories;
            _products = products;
            _discounts = discounts;
            _unitOfWork = unitOfWork;
            _files = files;
            _baseBeforeUpdateEntityLogics = baseBeforeUpdateEntityLogics;
            _identityManager = identityManager;
        }

        public async Task AddAsync(Product product)
        {
            _builder.SetBuildingEntity(product,
                _schemes.Query.Include(x => x.FieldDefenitions)
                    .Include(x => x.FieldDefenitionGroups)
                    .Include(x => x.FieldDefenitionGroups.Select(fdg => fdg.FieldDefenitions))
                    .First(x => x.Id == product.SchemeId),
                _productCategories.Query
                    .Include(x => x.FieldDefenitions)
                    .Include(x => x.FieldDefenitionGroups)
                    .Include(x => x.FieldDefenitionGroups.Select(fdg => fdg.FieldDefenitions))
                    .First(x => x.Id == product.PrimaryCategoryId)
            );

            product = _builder.GetResult();
            SanitizeAllowedQuantities(product);

            await HandleEshantionsAsync(product, product.Eshantions.ToList());
            await HandleRequiredProductsAsync(product, product.Eshantions.ToList());
            await HandleCrossSellingsAsync(product, product.CrossSellings.ToList());
            await HandleUpSellingsAsync(product, product.UpSellings.ToList());
            await HandleAssociatedProductsAsync(product, product.AssociatedProducts.ToList());
            HandleDownLoadFileIfItIs(product);

            await _products.AddAsync(product);
        }

        public async Task UpdateAsync(Product product, ProductAddOrUpdateModel updated)
        {
            await product.LoadDataByOptionsAsync(_unitOfWork);

            updated.MapToExisting(product);

            _builder.SetBuildingEntity(product,
                _schemes.Query
                    .Include(x => x.FieldDefenitions)
                    .Include(x => x.FieldDefenitionGroups)
                    .Include(x => x.FieldDefenitionGroups.Select(fdg => fdg.FieldDefenitions))
                    .First(x => x.Id == product.SchemeId),
                _productCategories.Query
                    .Include(x => x.FieldDefenitionGroups)
                    .Include(x => x.FieldDefenitionGroups.Select(fdg => fdg.FieldDefenitions))
                    .Include(x => x.FieldDefenitions)
                    .First(x => x.Id == product.PrimaryCategoryId)
            );
            HandleFields(product, updated.Fields);
            product = _builder.GetResult();

            SanitizeAllowedQuantities(product);
            await HandleEshantionsAsync(product, updated.Eshantions.MapToModel());
            await HandleRequiredProductsAsync(product, updated.RequiredProducts.MapToModel());
            await HandleCrossSellingsAsync(product, updated.CrossSellings.MapToModel());
            await HandleUpSellingsAsync(product, updated.UpSellings.MapToModel());
            await HandleAssociatedProductsAsync(product, updated.AssociatedProducts.MapToModel());
            HandleDownLoadFileIfItIs(product);
            HandlePictures(product);
            await _baseBeforeUpdateEntityLogics.ForEachAsync(x =>
                x.ApplyLogicAsync(product, _identityManager.GetCurrentPrincipal()));
            await _products.UpdateAsync(product);
        }
        private void HandleFields(Product product, List<FieldAddOrUpdateViewModel> newOnes)
        {
            var newOneIds = newOnes.Select(x => x.Id);
            product.Fields.Where(x => !newOneIds.Contains(x.Id)).ToList().ForEach(x =>
            {
                product.Fields.Remove(x);
                _unitOfWork.Delete(x);
            });
            newOnes.Where(x => x.Id == Guid.Empty).ToList().MapToModel().ForEach(x =>
            {
                x.ProductId = product.Id;
                _unitOfWork.Add(x);
                product.Fields.Add(x);
            });
            newOnes.Where(x => x.Id != Guid.Empty).ToList().ForEach(x =>
            {
                var old = product.Fields.First(o => o.Id == x.Id);
                x.MapToExisting(old);
                _unitOfWork.Update(old);
            });
        }


        public Task DeleteAsync(Product model)
        {
            return _products.DeleteAsync(model.Id);
        }

        private void HandleDownLoadFileIfItIs(Product model)
        {
            if (!model.IsDownLoadFile)
            {
                model.File = null;
                model.FileId = null;
                model.SampleFile = null;
                model.HasSampleFile = false;
                model.SampleFile = null;
            }
        }


        private void HandlePictures(Product model)
        {
            var updatedIds = model.Pictures.Where(x => x.Id != Guid.Empty).DistinctBy(x => x.Id).Select(x => x.Id)
                .Distinct().ToList();
            var updateds = model.Pictures.ToList();
            model.Pictures = _unitOfWork.Set<ProductPicture>().Where(x => x.ProductId == model.Id).ToList();
            model.Pictures.ToList().ForEach(x =>
            {
                if (updatedIds.Contains(x.Id))
                {
                    var updated = updateds.First(p => p.Id == x.Id);
                    x.AltAttribute = updated.AltAttribute;
                    x.TitleAttribute = updated.TitleAttribute;
                    x.DisplayOrder = updated.DisplayOrder;
                    _unitOfWork.Update(x);
                }
                else
                {
                    _unitOfWork.Delete(x);
                }
            });
            updateds.Where(x => x.Id == Guid.Empty).ToList().ForEach(model.Pictures.Add);
        }

        private async Task HandleAssociatedProductsAsync(Product model, List<Product> updatedAssociateds)
        {
            if (model.Type == ProductType.Grouped)
            {
                var associatedIds = updatedAssociateds.Where(x => x.Id != model.Id).DistinctBy(x => x.Id)
                    .Select(x => x.Id);
                model.AssociatedProducts.Clear();
                (await _products.Query.Where(x => associatedIds.Contains(x.Id)).ToListAsync()).ForEach(
                    model.AssociatedProducts.Add);
                model.Price = 0;
                model.OldPrice = 0;
                model.ProductCost = 0;
            }
            else
            {
                model.AssociatedProducts.Clear();
            }
        }

        private async Task HandleUpSellingsAsync(Product model, List<Product> upSellings)
        {
            if (model.RelatedProductLoadType == RelatedProductLoadType.Manually)
            {
                var upSelledIds = upSellings.Where(x => x.Id != model.Id).DistinctBy(x => x.Id).Select(x => x.Id);
                model.UpSellings.Clear();
                (await _products.Query.Where(x => upSelledIds.Contains(x.Id)).ToListAsync()).ForEach(model.UpSellings
                    .Add);
            }
            else
            {
                model.UpSellings.Clear();
            }
        }

        private async Task HandleCrossSellingsAsync(Product model, List<Product> updatedCrossSellings)
        {
            if (model.HasCrossSelledProducts)
            {
                var crossSelledIds = updatedCrossSellings.Where(x => x.Id != model.Id).DistinctBy(x => x.Id)
                    .Select(x => x.Id);
                model.CrossSellings.Clear();
                (await _products.Query.Where(x => crossSelledIds.Contains(x.Id)).ToListAsync()).ForEach(
                    model.CrossSellings.Add);
            }
            else
            {
                model.CrossSellings.Clear();
            }
        }


        private async Task HandleRequiredProductsAsync(Product model, List<Product> requiredProducts)
        {
            if (model.HasRequiredProducts)
            {
                var requiredIds = requiredProducts.DistinctBy(x => x.Id).Where(x => x.Id != model.Id).Select(x => x.Id);
                model.RequiredProducts.Clear();
                (await _products.Query.Where(x => requiredIds.Contains(x.Id)).ToListAsync()).ForEach(
                    model.RequiredProducts.Add);
            }
            else
            {
                model.RequiredProducts.Clear();
            }
        }

        private async Task HandleEshantionsAsync(Product model, List<Product> updatedEshantions)
        {
            if (model.HasEshantion)
            {
                var eshantionIds = updatedEshantions.DistinctBy(x => x.Id).Where(x => x.Id != model.Id)
                    .Select(x => x.Id);
                model.Eshantions.Clear();
                (await _products.Query.Where(x => eshantionIds.Contains(x.Id)).ToListAsync()).ForEach(model.Eshantions
                    .Add);
            }
            else
            {
                model.Eshantions.Clear();
            }
        }


        private void SanitizeAllowedQuantities(Product product)
        {
            if (string.IsNullOrEmpty(product.Inventory.AllowedQuantities)) return;
            var items = product.Inventory.AllowedQuantities.Split(',').ToList();
            var allowedItems = new List<string>();
            items.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x)) allowedItems.Add(x);
            });
            product.Inventory.AllowedQuantities = string.Join(",", allowedItems);
        }
    }
}