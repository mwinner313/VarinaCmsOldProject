using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoMapper;
using VarinaCmsV2.Core.Contracts.WebClientServices.Products;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class ProductMapHelper
    {
        public static Product MapToModel(this ProductAddOrUpdateModel productVm)
        {
            return Mapper.Map<Product>(productVm);
        }
        public static List<Product> MapToModel(this List<ProductViewModel> productVms)
        {
            return Mapper.Map<List<Product>>(productVms);
        }
        public static List<Product> MapToModel(this List<ProductIdModel> productVms)
        {
            return Mapper.Map<List<Product>>(productVms);
        }
        public static Product MapToExisting(this ProductAddOrUpdateModel productVm, Product existing)
        {
            return Mapper.Map(productVm, existing);
        }
        public static ProductViewModel MapToViewModel(this Product productVm)
        {
            var vm = Mapper.Map<ProductViewModel>(productVm);
            vm.Pictures = vm.Pictures.OrderBy(x => x.DisplayOrder).ToList();
            vm.Scheme.FieldDefenitions = vm.Scheme.FieldDefenitions.OrderBy(x => x.Order).ToList();
            vm.Scheme.FieldDefenitionGroups.ForEach(fdg => fdg.FieldDefenitions = fdg.FieldDefenitions.OrderBy(x => x.Order).ToList());
            vm.PrimaryCategory.FieldDefenitions = vm.PrimaryCategory.FieldDefenitions.OrderBy(x => x.Order).ToList();
            vm.PrimaryCategory.FieldDefenitionGroups.ForEach(fdg => fdg.FieldDefenitions = fdg.FieldDefenitions.OrderBy(x => x.Order).ToList());
            vm.Fields = vm.Fields.OrderBy(x => x.Fd.Order).ToList();
            return vm;
        }
        public static ProductPicture MapToViewModel(this ProductPictureViewModel vm)
        {
            return Mapper.Map<ProductPicture>(vm);
        }

        public static List<ProductPicture> MapToViewModel(this List<ProductPictureViewModel> vm)
        {
            return Mapper.Map<List<ProductPicture>>(vm);
        }

        public static ProductLiquidAdapter AsLiquidAdapted(this Product product)
        {
            return Mapper.Map<ProductLiquidAdapter>(product);
        }

        public static ProductsCompareModelLiquidVeiwModel MapToLiquidViewModel(this ProductsCompareModel product)
        {
            return Mapper.Map<ProductsCompareModelLiquidVeiwModel>(product);
        }

        public static SearchProductResultLiquidViewModel MapToLiquidView(this SearchProductResult res)
        {
            var products = Mapper.Map<List<ProductLiquidAdapter>>(res.Products);
            return new SearchProductResultLiquidViewModel()
            {
                Count = res.Count,
                Products = products,
                SearchText = res.SearchText
            };
        }
        public static List<ProductLiquidAdapter> AsLiquidAdapted(this List<Product> product)
        {
            return Mapper.Map<List<ProductLiquidAdapter>>(product);
        }
        public static List<ProductViewModel> MapToViewModel(this IEnumerable<Product> productVm)
        {
            var vms = Mapper.Map<List<ProductViewModel>>(productVm);
            vms.ForEach(x =>
            {
                x.Pictures = x.Pictures.OrderBy(p => p.DisplayOrder).ToList();
                x.PrimaryImage = x.Pictures.FirstOrDefault();
            });
            return vms;
        }

      
    }
}
