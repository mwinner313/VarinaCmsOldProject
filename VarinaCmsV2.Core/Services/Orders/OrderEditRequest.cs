using System;
using System.Security.Principal;
using VarinaCmsV2.ViewModel.Eshop.Orders;

namespace VarinaCmsV2.Core.Services.Orders
{
    public class OrderEditRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public Guid Id { get; set; }
        public OrderAddOrUpdateViewModel Model { get; set; }
    }
}