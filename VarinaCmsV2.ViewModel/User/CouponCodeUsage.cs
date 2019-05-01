using System;
using DotLiquid;

namespace VarinaCmsV2.ViewModel.User
{
    public class CouponCodeUsage:Drop
    {
        public Guid DisocuntId { get; set; }
        public string Code { get; set; }
        public bool Expired { get; set; }
        public string DiscountName { get; set; }
        public string Message { get; set; }
    }
}