using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using PersianDate;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Services.Orders;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.DomainClasses.Entities.EShop.Shipping;

namespace VarinaCmsV2.Core.Logic.Services.Orders
{
    public class OrderLogger : IOrderLogger
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderLogger(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OrderUpdated(Order oldValues, Order newValues)
        {
            if (oldValues.PaymentStatus != newValues.PaymentStatus)
            {
                AddOrderLog($"وضعیت پرداخت از {GetPaymentStatusName(oldValues.PaymentStatus)} به {GetPaymentStatusName(newValues.PaymentStatus)} تغییر یافت.", newValues.Id);
            }
            if (oldValues.ShippingStatus != newValues.ShippingStatus)
            {
                AddOrderLog($"وضعیت ارسال از {GetShippingStatusName(oldValues.ShippingStatus)} به {GetShippingStatusName(newValues.ShippingStatus)} تغییر یافت.", newValues.Id);
            }
            if (oldValues.Comment != newValues.Comment)
            {
                AddOrderLog("نظر ادمین تغییر یافت.", newValues.Id);
            }
            if (newValues.Shipment != null && oldValues.Shipment != null)
                HandleShipmentChanges(oldValues.Shipment, newValues.Shipment, oldValues.Id);
        }

        private void HandleShipmentChanges(Shipment oldShipment, Shipment updatedShipment, Guid orderId)
        {
            if (oldShipment is null)
            {
                if (updatedShipment.DeliveryDate.HasValue)
                {
                    AddOrderLog($"تاریخ رسیدن به محصول به دست مشتری به {ConvertDate.ToFa(updatedShipment.DeliveryDate, "yyyy/MM/dd - hh:mm")}  تغییر یافت.", orderId);
                }
                if (updatedShipment.ShippedDate.HasValue)
                {
                    AddOrderLog($"تاریخ ارسال مرسوله به {ConvertDate.ToFa(updatedShipment.DeliveryDate, "yyyy/MM/dd - hh:mm")}  تغییر یافت.", orderId);
                }
                if (!string.IsNullOrEmpty(updatedShipment.AdminComment))
                {
                    AddOrderLog($"نظر ادمین برای حمل و نقل تغییر یافت", orderId);
                }
                if (!string.IsNullOrEmpty(updatedShipment.TrackingNumber))
                {
                    AddOrderLog($"شماره پیگیری حمل و نقل تغییر یافت", orderId);
                }
            }
            else
            {
                if (updatedShipment.DeliveryDate != oldShipment.DeliveryDate)
                {
                    AddOrderLog($"تاریخ رسیدن محصول به دست مشتری از {ConvertDate.ToFa(oldShipment.DeliveryDate, "yyyy/MM/dd - hh:mm")} به {ConvertDate.ToFa(updatedShipment.DeliveryDate, "yyyy/MM/dd - hh:mm")} تغییر یافت.", orderId);
                }
                if (updatedShipment.ShippedDate != oldShipment.ShippedDate)
                {
                    AddOrderLog($"تاریخ ارسال مرسوله از {ConvertDate.ToFa(oldShipment.ShippedDate, "yyyy/MM/dd - hh:mm")} به {ConvertDate.ToFa(updatedShipment.ShippedDate, "yyyy/MM/dd - hh:mm")}  تغییر یافت.", orderId);
                }
                if (updatedShipment.AdminComment != oldShipment.AdminComment)
                {
                    AddOrderLog($"نظر ادمین برای حمل و نقل تغییر یافت", orderId);
                }
                if (updatedShipment.TrackingNumber != oldShipment.TrackingNumber)
                {
                    AddOrderLog($"شماره پیگیری حمل و نقل از {oldShipment.TrackingNumber} به {updatedShipment.TrackingNumber} تغییر یافت.", orderId);
                }
            }
        }
        private string GetShippingStatusName(ShippingStatus status)
        {
            switch (status)
            {
                case ShippingStatus.Delivered: return "رسیده به دست مشتری";
                case ShippingStatus.NotYetShipped: return "هنوز ارسال نشده";
                case ShippingStatus.Shipped: return "ارسال شده";
                case ShippingStatus.ShippingNotRequired: return "ارسال نیاز نمیباشد";
            }
            return null;
        }

        private string GetPaymentStatusName(PaymentStatus status)
        {
            switch (status)
            {
                case PaymentStatus.Paid: return "پرداخت شده";
                case PaymentStatus.Pending: return "در انتظار";
            }
            return null;
        }


        public void OrderItemDownloadActivateStateChanged(OrderItem downloadItem)
        {
            AddOrderLog(
                downloadItem.IsDownloadActivated
                    ? $"لینک دانلود محصول با شناسه {downloadItem.ProductId} فعال شد."
                    : $"لینک دانلود محصول با شناسه {downloadItem.ProductId} غیر فعال شد.", downloadItem.OrderId);
        }

        private void AddOrderLog(string message, Guid orderId)
        {
            _unitOfWork.Set<OrderLog>().Add(new OrderLog()
            {
                OrderId = orderId,
                CreatorId = HttpContext.Current.User.Identity.GetUserId().ToGuid(),
                Log = message,
            });
        }
    }
}
