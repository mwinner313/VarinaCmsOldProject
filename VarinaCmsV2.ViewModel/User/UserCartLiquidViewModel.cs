using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.Eshop;
using VarinaCmsV2.ViewModel.Eshop.Discount;
using VarinaCmsV2.ViewModel.User.Account;

namespace VarinaCmsV2.ViewModel.User
{
    public class UserCartLiquidViewModel:LiquidAdapter
    {
        public UserLiquidViewModel User { get; set; }
        public List<ShoppingCartItemLiquidViewModel> ShoppingCartItems { get; set; }
        public long TotalPrice { get; set; }
        public List<ProductLiquidAdapter> CrossSellings { get; set; }
        public List<CouponCodeUsage> CouponCodeUsages { get; set; }
        public List<ProductLiquidAdapter> Eshantions { get; set; }
    }
}