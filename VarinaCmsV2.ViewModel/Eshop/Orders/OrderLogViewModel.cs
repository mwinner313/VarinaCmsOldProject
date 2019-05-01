using System;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.ViewModel.Eshop.Orders
{
    public class OrderLogViewModel
    {
        public UserWebClientViewModel Creator { get; set; }
        public string Log { get; set; }
        public string CreateDateTime { get; set; }
    }
}