using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.WebClientServices.Products;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class ProductCategoryMapHelper
    {
        public static IList<ProductCategoryViewModel> MapToViewModel(this IList<ProductCategory> categories)
        {
            return Mapper.Map<List<ProductCategoryViewModel>>(categories);
        }
        public static IList<ProductCategory> MapToModel(this IList<ProductCategoryViewModel> categories)
        {
            return Mapper.Map<List<ProductCategory>>(categories);
        }
        public static ProductCategoryViewModel MapToViewModel(this ProductCategory cat)
        {
            return Mapper.Map<ProductCategoryViewModel>(cat);
        }
        public static ProductCategory MapToModel(this ProductCategoryAddOrUpdateModel cat)
        {
            return Mapper.Map<ProductCategory>(cat);
        }
        public static async Task<List<ProductCategoryViewModel>> ToViewModelListIncludeMetaDataAsync(this IQueryable<ProductCategory> model, IMetaDataDataService metaDataService)
        {
            var unMappeds = await model.ToListAsync();
            var items = Mapper.Map<List<ProductCategoryViewModel>>(unMappeds);
            var ids = items.SelectDeep(x => x.Id);
            metaDataService.Query.Where(x => ids.Contains(x.TargetId) && x.TargetType == ProductCategory.MetaTypeName).ToList().ForEach(x =>
            {
                items.ForEachDeep(c =>
                {
                    if (c.Id == x.TargetId)
                        c.MetaData.Add(x.MapToViewModel());
                });
            });
            return items;
        }

        public static List<T> SelectDeep<T>(this IEnumerable<ProductCategoryViewModel> queriable, Func<ProductCategoryViewModel, T> select)
        {
            List<T> list = new List<T>();
            foreach (var x in queriable)
            {
                list.Add(select(x));
                if (x.Childs != null && x.Childs.Count != 0)
                    list.AddRange(x.Childs.SelectDeep(select));
            }
            return list;
        }
        public static void ForEachDeep(this IEnumerable<ProductCategoryViewModel> queriable, Action<ProductCategoryViewModel> action)
        {

            foreach (var x in queriable)
            {
                action(x);
                if (x.Childs != null && x.Childs.Count != 0)
                    x.Childs.ForEachDeep(action);
            }
        }
        public static void MapToExisting(this ProductCategoryAddOrUpdateModel cat, ProductCategory existingCategory)
        {
            Mapper.Map(cat, existingCategory);
        }

        public static ProductCategoryLiquidVeiwModel ToLiquidViewModel(this ProductCategory cat)
        {
            return Mapper.Map<ProductCategoryLiquidVeiwModel>(cat);
        }

        public static ProductCategoryLiquidVeiwModel AsLiquidAdapted(this PaginatedProductCategory model)
        {
            var adaptedCat = Mapper.Map<ProductCategoryLiquidVeiwModel>(model.ProductCategory);
            adaptedCat.Products = model.Products.AsLiquidAdapted();
            adaptedCat.AllPagesCount = model.AllPagesCount;
            adaptedCat.CurrentPage = model.CurrentPage;
            return adaptedCat;
        }
        public static ProductTagLiquidViewModel AsLiquidAdapted(this PaginatedProducTag model)
        {
            var tag = Mapper.Map<ProductTagLiquidViewModel>(model.Tag);
            tag.Products = model.Products.AsLiquidAdapted();
            tag.AllPagesCount = model.AllPagesCount;
            tag.CurrentPage = model.CurrentPage;
            return tag;
        }
    }
}
