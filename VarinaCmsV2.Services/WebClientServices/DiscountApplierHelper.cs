using System;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;

namespace VarinaCmsV2.Services.WebClientServices
{
    public class DiscountApplierHelper
    {
        public void ApplyOnOrder(Order order, Discount discount)
        {
            switch (discount.DiscountType)
            {
                case DiscountType.AssignedToOrderSubTotal:
                    ApplyDiscountBaseOnOrderTotal(order, discount);
                    break;
                case DiscountType.AssignedToShipping:
                    ApplyDiscountBaseOnShipping(order, discount);
                    break;
            }
        }
        public void ApplyOnOrderItem(OrderItem item, Discount discount)
        {
            long amount = 0;
            if (discount.UsePercentage)
                amount = (item.Product.Price / 100) * discount.DiscountPercentage;
            else
                amount = discount.DiscountAmount;
            if (discount.MaximumDiscountedQuantity.HasValue &&
                discount.MaximumDiscountedQuantity.Value < item.Quantity)
            {
                item.DiscountAmount += discount.MaximumDiscountedQuantity.Value * amount;
            }
            else
            {
                item.DiscountAmount += amount * item.Quantity;
            }
        }
        private void ApplyDiscountBaseOnShipping(Order order, Discount discount)
        {
            throw new NotImplementedException();
        }

        private void ApplyDiscountBaseOnOrderTotal(Order order, Discount discount)
        {
            long amount = 0;
            if (discount.UsePercentage)
                amount = (order.OrderSubTotal / 100) * discount.DiscountPercentage;
            else
                amount = discount.DiscountAmount;
            order.OrderDiscount = amount;

        }
       
    }
}
