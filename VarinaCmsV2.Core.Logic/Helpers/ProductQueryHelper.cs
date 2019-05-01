using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Product;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class ProductQueryHelper
    {
        public static IQueryable<Product> FilterByProductQuery(this IQueryable<Product> queryable,
            ProductQuery filterOptions)
        {
            queryable = FilterByTitle(queryable, filterOptions.Title);
            queryable = FilterBySku(queryable, filterOptions.Sku);
            queryable = FilterByTagIds(queryable, filterOptions.TagIds);
            queryable = FilterByScheme(queryable, filterOptions);
            queryable = FilterByCategoryId(queryable, filterOptions);
            queryable = filterOptions.LowStocks ? FilterByLowStock(queryable) : queryable;
            queryable = filterOptions.HasEshantion ? FilterByHasEshantion(queryable, filterOptions) : queryable;
            queryable = filterOptions.AvailibleForPreOrder ? queryable.Where(x => x.AvailableForPreOrder) : queryable;
            queryable = FilterByPublishState(queryable, filterOptions.PublishState);
            queryable = FilterByProductType(queryable, filterOptions.ProductType);


            return queryable.ApplyProductOrderBy(filterOptions.Order);
        }
        private static IQueryable<Product> FilterByPublishState(IQueryable<Product> query, PublishState state)
        {
            var now = DateTime.Now.AddMinutes(3);
            switch (state)
            {
                case PublishState.NotPublished: return query.Where(x => x.PublishDateTime > now ||!x.IsVisible);
                case PublishState.Published:
                    return query.Where(x => x.PublishDateTime < now && x.IsVisible);
                default: return query;
            }
        }
        //maby later
        //private static IQueryable<Product> FilterByRequiredProducts(IQueryable<Product> queryable, ProductQuery filterOptions)
        //{
        //    var products = queryable.Where(x => x.HasRequiredProducts);
        //    return filterOptions.RequiredProductIds != null && filterOptions.RequiredProductIds.Count != 0
        //        ? products.Where(x => x.RequiredProducts.Any(d => filterOptions.RequiredProductIds.Contains(d.Id)))
        //        : products;
        //}



        private static IQueryable<Product> FilterByHasEshantion(IQueryable<Product> queryable,
            ProductQuery filterOptions)
        {
            var products = queryable.Where(x => x.HasEshantion);
            return filterOptions.EshantionIds != null && filterOptions.EshantionIds.Count != 0
                ? products.Where(x => x.Eshantions.Any(d => filterOptions.EshantionIds.Contains(d.Id)))
                : products;
        }
        //maby later
        //private static IQueryable<Product> FilterByDiscount(IQueryable<Product> queryable, ProductQuery filterOptions)
        //{
        //    var products = queryable.Where(x => x.HasDiscountsApplied);
        //    return filterOptions.RelatedCategoryIds != null && filterOptions.RelatedCategoryIds.Count != 0
        //        ? products.Where(x => x.AppliedDiscounts.Any(d => filterOptions.DiscountIds.Contains(d.Id)))
        //        : products;
        //}

        private static IQueryable<Product> FilterBySku(IQueryable<Product> queryable, string sku)
        {
            return string.IsNullOrEmpty(sku) ? queryable : queryable.Where(x => x.Sku.Contains(sku));
        }

        public static IOrderedQueryable<Product> ApplyProductOrderBy(this IQueryable<Product> queryable, string order)
        {
            switch (order)
            {
                case "stock_quantity_asc": return queryable.OrderBy(x => x.Inventory.StockQuantity);
                case "stock_quantity_desc": return queryable.OrderByDescending(x => x.Inventory.StockQuantity);
                case "create_date_asc": return queryable.OrderBy(x => x.CreateDateTime);
                case "create_date_desc": return queryable.OrderByDescending(x => x.CreateDateTime);
                case "update_date_asc": return queryable.OrderBy(x => x.UpdateDateTime);
                case "publish_date_asc": return queryable.OrderBy(x => x.PublishDateTime);
                case "update_date_desc": return queryable.OrderByDescending(x => x.UpdateDateTime);
                case "publish_date_desc": return queryable.OrderByDescending(x => x.PublishDateTime);
                case "title_asc": return queryable.OrderBy(x => x.Title);
                case "title_desc": return queryable.OrderByDescending(x => x.CreateDateTime);
                case "visit_count_asc": return queryable.OrderBy(x => x.VisitCount);
                case "like_count_asc": return queryable.OrderBy(x => x.LikeCount);
                case "visit_count_desc": return queryable.OrderByDescending(x => x.VisitCount);
                case "like_count_desc": return queryable.OrderByDescending(x => x.LikeCount);
                default: return queryable.OrderByDescending(x => x.CreateDateTime);
            }
        }

        private static IQueryable<Product> FilterByProductType(IQueryable<Product> queryable, ProductTypeOption type)
        {
            switch (type)
            {
                case ProductTypeOption.Grouped: return queryable.Where(x => x.Type == ProductType.Grouped);
                case ProductTypeOption.Simple: return queryable.Where(x => x.Type == ProductType.Simple);
                default: return queryable;
            }
        }

        private static IQueryable<Product> FilterByLowStock(IQueryable<Product> queryable)
        {
            return queryable.Where(x => x.Inventory.StockQuantity < x.Inventory.MinStockQuantity);
        }

        private static IQueryable<Product> FilterByTitle(IQueryable<Product> queryable, string title)
        {
            return string.IsNullOrEmpty(title) ? queryable : queryable.Where(x => x.Title.Contains(title));
        }

        private static IQueryable<Product> FilterByTagIds(IQueryable<Product> productQuery, List<Guid> tagIds)
        {
            return tagIds != null && tagIds.Count != 0
                ? productQuery.Where(x => x.Tags.Any(t => tagIds.Contains(t.Id)))
                : productQuery;
        }

        private static IQueryable<Product> FilterByScheme(IQueryable<Product> productQuery, ProductQuery query)
        {
            return string.IsNullOrEmpty(query.Scheme) && !query.SchemeId.HasValue
                ? productQuery
                : productQuery.Where(x => x.SchemeId == query.SchemeId || x.Scheme.Handle == query.Scheme);
        }

        private static IQueryable<Product> FilterByCategoryId(IQueryable<Product> productQuery, ProductQuery query)
        {
            if (query.PrimaryCategoryId == Guid.Empty) return productQuery;

            //todo deep search for parentid
            productQuery = query.IncludeChildCategories
                ? productQuery.Where(x => x.PrimaryCategory.ParentId == query.PrimaryCategoryId || x.PrimaryCategoryId == query.PrimaryCategoryId)
                : productQuery.Where(x => x.PrimaryCategoryId == query.PrimaryCategoryId);
            productQuery = query.RelatedCategoryIds != null && query.RelatedCategoryIds.Count != 0
                ? productQuery.Where(x => x.RelatedCategories.Any(r => query.RelatedCategoryIds.Contains(r.Id)))
                : productQuery; 
            return productQuery;
        }


        public static IQueryable<Product> IncludeNecessaryData(this IQueryable<Product> query)
        {
            return query
                .Include(x => x.Pictures)
                .Include(x => x.ProductReviews)
                .Include(x => x.Fields)
                .Include(x => x.RelatedCategories)
                .Include(x => x.Creator)
                .Include(x => x.Tags)
                .Include(x => x.Fields.Select(f => f.FieldDefenition));
        }

        public static async Task LoadDataByOptionsAsync(this Product product, IUnitOfWork unitOfWork)
        {
            if (product.HasCrossSelledProducts)
            {
                await unitOfWork.Entry(product).Collection(x => x.CrossSellings).LoadAsync();
            }
            if (product.HasEshantion)
            {
                await unitOfWork.Entry(product).Collection(x => x.Eshantions).LoadAsync();
            }
            if (product.HasRequiredProducts)
            {
                await unitOfWork.Entry(product).Collection(x => x.RequiredProducts).LoadAsync();
            }
            if (product.Type == ProductType.Grouped)
            {
                await unitOfWork.Entry(product).Collection(x => x.AssociatedProducts).LoadAsync();
            }
            if (product.Shipment.IsShipEnabled)
                await unitOfWork.Entry(product).Collection(x => x.DeliveryDates).LoadAsync();
            if(product.RelatedProductLoadType==RelatedProductLoadType.Manually)
                await unitOfWork.Entry(product).Collection(x => x.UpSellings).LoadAsync();

            if (product.IsDownLoadFile)
                await unitOfWork.Entry(product).Reference(x => x.File).LoadAsync();
            if (product.HasSampleFile)
                await unitOfWork.Entry(product).Reference(x => x.SampleFile).LoadAsync();

        }
    }
}