using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Core.CustomAttributes
{
    public class UserAttributes
    {
        public const string SelectedShippingMethodId = nameof(SelectedShippingMethodId);
        public const string SelectedShippingAddressId = nameof(SelectedShippingAddressId);
        public const string PaymentProccessingOrderId = nameof(PaymentProccessingOrderId);
        public const string CouponCodeUsage = nameof(CouponCodeUsage);
        public const string SelectedPaymentMethod= nameof(SelectedPaymentMethod);
    }
}
