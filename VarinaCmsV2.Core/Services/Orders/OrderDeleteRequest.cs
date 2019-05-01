using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Orders
{
    public class OrderDeleteRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public Guid Id { get; set; }
    }
}