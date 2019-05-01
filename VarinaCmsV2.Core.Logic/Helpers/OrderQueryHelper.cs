using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Services.Orders;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.DomainClasses.Entities.EShop.Shipping;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class OrderQueryHelper
    {
        public static IQueryable<Order> FilterByQuery(this IQueryable<Order> orders, OrderQuery query)
        {
            if (!string.IsNullOrEmpty(query.FromDate))
            {
                var fromDate = DateHelper.ParseSafe(query.FromDate);
                orders = orders.Where(x => x.CreateDateTime > fromDate);
            }
            if (!string.IsNullOrEmpty(query.ToDate))
            {
                var toDate = DateHelper.ParseSafe(query.ToDate);
                orders = orders.Where(x => x.CreateDateTime < toDate);
            }
            if (!string.IsNullOrEmpty(query.ProductTitle))
            {
                orders = orders.Where(x => x.OrderItems.Any(i => i.Product.Title.Contains(query.ProductTitle)));
            }
            if (!string.IsNullOrEmpty(query.UserName))
            {
                orders = orders.Where(x => x.Creator.UserName.Contains(query.UserName));
            }
            switch (query.OrderStatus)
            {
                case OrderStatusOption.Cancelled:
                    orders = orders.Where(x => x.OrderStatus == OrderStatus.Cancelled); break;
                case OrderStatusOption.Complete:
                    orders = orders.Where(x => x.OrderStatus == OrderStatus.Complete); break;
                case OrderStatusOption.Pending:
                    orders = orders.Where(x => x.OrderStatus == OrderStatus.Pending); break;
                case OrderStatusOption.Processing:
                    orders = orders.Where(x => x.OrderStatus == OrderStatus.Processing); break;
            }

            switch (query.PaymentStatus)
            {
                case PaymentStatusOption.Paid:
                    orders = orders.Where(x => x.PaymentStatus == PaymentStatus.Paid); break;
                case PaymentStatusOption.Pending:
                    orders = orders.Where(x => x.PaymentStatus == PaymentStatus.Pending); break;
            }
            switch (query.ShippingStatus)
            {
                case ShippingStatusOption.Delivered:
                    orders = orders.Where(x => x.ShippingStatus == ShippingStatus.Delivered); break;
                case ShippingStatusOption.NotYetShipped:
                    orders = orders.Where(x => x.ShippingStatus == ShippingStatus.NotYetShipped); break;
                case ShippingStatusOption.Shipped:
                    orders = orders.Where(x => x.ShippingStatus == ShippingStatus.Shipped); break;
                case ShippingStatusOption.ShippingNotRequired:
                    orders = orders.Where(x => x.ShippingStatus == ShippingStatus.ShippingNotRequired); break;
            }

            if (query.NotActivatedDownloads)
            {
                orders = orders.Where(x => x.OrderItems.Any(i => i.Product.IsDownLoadFile && !i.IsDownloadActivated && i.Product.DownLoadActivationType == ProductDownLoadActivationType.Manually));
            }

            if (query.NotSeen)
            {
                orders = orders.Where(x => !x.SeenByAdmin);
            }

            return orders;
        }
    }
}
