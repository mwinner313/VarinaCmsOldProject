using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Category
{
    public class CategoryDeleteRequest:IServiceRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public Guid Id { get; set; }
    }
}