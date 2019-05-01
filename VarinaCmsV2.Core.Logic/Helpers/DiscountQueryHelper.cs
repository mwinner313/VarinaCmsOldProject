using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Services.Discounts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class DiscountQueryHelper
    {
        public static IQueryable<Discount> FilterByQuery(this IQueryable<Discount> discounts, DiscountQuery query)
        {
            if (!string.IsNullOrEmpty(query.Name))
                discounts = discounts.Where(x => x.Name.Contains(query.Name));
            if (!string.IsNullOrEmpty(query.CouponCode))
                discounts = discounts.Where(x => x.CouponCode.Contains(query.CouponCode));
            if (query.DiscountType.HasValue)
                discounts = discounts.Where(x => x.DiscountType == query.DiscountType);

            return discounts;
        }

        public static IQueryable<Discount> FilterByAvailibilty(this IQueryable<Discount> queryable)
        {
            queryable = queryable.Where(x => x.StartDate > DateTime.Now || !x.StartDate.HasValue);
            queryable = queryable.Where(x => x.EndDate < DateTime.Now || !x.EndDate.HasValue);
            return queryable;
        }
    }
}
