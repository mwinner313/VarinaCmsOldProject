using System;
using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Core.Services.CurrentUser
{
    public class UserAddressesResponse
    {
        public Guid? SelectedAddressId { get; set; }
        public List<Address> Addresses { get; set; }
    }
}