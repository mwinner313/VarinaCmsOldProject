using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;

namespace VarinaCmsV2.Services.Helpers
{
    public static  class DiscountQueryHelper
    {
        public static IQueryable<Discount> FilterByAvailibilty(this IQueryable<Discount> queryable)
        {
            queryable = queryable.Where(x => x.StartDate > DateTime.Now || !x.StartDate.HasValue);
            queryable = queryable.Where(x => x.EndDate < DateTime.Now || !x.EndDate.HasValue);
            return queryable;
        }
    }
}
