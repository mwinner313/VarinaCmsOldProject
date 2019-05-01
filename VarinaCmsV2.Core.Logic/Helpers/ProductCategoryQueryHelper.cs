using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Services.ProductCategories;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class ProductCategoryQueryHelper
    {
        public static IQueryable<ProductCategory> FilterByQuery(this IQueryable<ProductCategory> categories,
            ProductCategoryQuery query)
        {
            return categories.FilterByCategoryType(query).FilterByMapTreeView(query.MapForTreeView);
        }

        private static IQueryable<ProductCategory> FilterByCategoryType(this IQueryable<ProductCategory> categories,
            ProductCategoryQuery query)
        {
            return query.CheckByType ? categories.Where(x => x.CategoryType == query.CategoryType || x.CategoryType == CategoryType.Mixed) : categories;
        }
        private static IQueryable<ProductCategory> FilterByMapTreeView(this IQueryable<ProductCategory> categories,
            bool mapForTreeView)
        {
            return mapForTreeView ? categories.Where(x => !x.ParentId.HasValue) : categories;
        }
    }
}
