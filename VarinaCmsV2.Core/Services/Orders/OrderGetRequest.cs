using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Orders
{
    public class OrderGetRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public Guid OrderId { get; set; }
    }
}