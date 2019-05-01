using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Services.Category;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.Enums;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class CategoryQueryHelper
    {
        public static IQueryable<Category> FilterByQuery(this IQueryable<Category> categoryQuery, CategoryQuery query, IUnitOfWork unitOfWork)
        {
            categoryQuery = FilterByScheme(categoryQuery, query.Scheme);
            categoryQuery = FilterBySearchText(categoryQuery, query.SearchText);
            categoryQuery = FilterByType(categoryQuery, query);
            categoryQuery = HandleMapingForView(categoryQuery, query, unitOfWork);

            return categoryQuery;
        }



        private static IQueryable<Category> FilterByType(IQueryable<Category> categoryQuery, CategoryQuery query)
        {
            if (query.CheckByType && query.CategoryType == CategoryType.Main)
                return
                    categoryQuery.Where(x => x.CategoryType == CategoryType.Main || x.CategoryType == CategoryType.Mixed);
            if (query.CheckByType && query.CategoryType == CategoryType.Secendary)
                return
                    categoryQuery.Where(x => x.CategoryType == CategoryType.Secendary || x.CategoryType == CategoryType.Mixed);

            return categoryQuery;
        }

        private static IQueryable<Category> FilterBySearchText(IQueryable<Category> categoryQuery, string searchText)
        {
            return string.IsNullOrEmpty(searchText) ? categoryQuery :
                categoryQuery.Where(
                    x =>
                        x.Handle.Contains(searchText) || x.Title.Contains(searchText) ||
                        x.UrlSegment.Contains(searchText));
        }

        private static IQueryable<Category> FilterByScheme(IQueryable<Category> categoryQuery, string scheme)
        {
            return string.IsNullOrEmpty(scheme)? categoryQuery : categoryQuery.Where(x => x.Scheme.Handle == scheme);
        }

        private static IQueryable<Category> HandleMapingForView(IQueryable<Category> categoryQuery, CategoryQuery query, IUnitOfWork unitOfWork)
        {
            if (!query.MapForTreeView) return categoryQuery;
            categoryQuery = categoryQuery.OrderBy(x => x.Order).Where(x => !x.ParentId.HasValue);
            return categoryQuery;
        }
       
    }
}
