using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.Core.Logic.Widgets
{
    internal static class WidgetExtention 
    {
        internal static IQueryable<T> GetWithApliedTimeRange<T>(this IQueryable<T> queryable, TimeRangeOption opt) where T :IScheduledItem
        {
            var maxDate = DateTime.MinValue;
            switch (opt)
            {
                case TimeRangeOption.OneMonth:
                    maxDate = DateTime.Now.AddMonths(-1); break;
                case TimeRangeOption.TwoMonth:
                    maxDate = DateTime.Now.AddMonths(-2); break;
                case TimeRangeOption.ThreeMonth:
                    maxDate = DateTime.Now.AddMonths(-3); break;
                case TimeRangeOption.OneWeek:
                    maxDate = DateTime.Now.AddDays(-7); break;
                case TimeRangeOption.TwoWeek:
                    maxDate = DateTime.Now.AddDays(-14); break;
                case TimeRangeOption.ThreeWeek:
                    maxDate = DateTime.Now.AddDays(-21); break;
                default: break;
            }

            return queryable.Where(x => x.PublishDateTime > maxDate );
        }

        internal static IQueryable<DomainClasses.Entities.Entity> GetWithAppliedOrder(this IQueryable<DomainClasses.Entities.Entity> entities, OrderOption opt)
        {
            switch (opt)
            {
                case OrderOption.ByDateDecending:
                    return entities.OrderByDescending(x => x.PublishDateTime);
                case OrderOption.LikesCount:
                    return entities.OrderByDescending(x => x.LikeCount);
                case OrderOption.VisitNumber:
                    return entities.OrderByDescending(x => x.VisitCount);
                case OrderOption.Random:
                    return entities.OrderByDescending(x => Guid.NewGuid()).Distinct();
                default: return entities;
            }
        }
        internal static IQueryable<Product> GetWithAppliedOrder(this IQueryable<Product> entities, OrderOption opt)
        {
            switch (opt)
            {
                case OrderOption.ByDateDecending:
                    return entities.OrderByDescending(x => x.PublishDateTime);
                case OrderOption.LikesCount:
                    return entities.OrderByDescending(x => x.LikeCount);
                case OrderOption.VisitNumber:
                    return entities.OrderByDescending(x => x.VisitCount);
                case OrderOption.Random:
                    return entities.OrderByDescending(x => Guid.NewGuid()).Distinct();
                default: return entities;
            }
        }

    }
}
