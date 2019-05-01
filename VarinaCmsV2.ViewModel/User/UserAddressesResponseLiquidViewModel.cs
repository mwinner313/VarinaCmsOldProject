using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.ViewModel.User
{
    public class UserAddressesResponseLiquidViewModel:LiquidAdapter
    {
        public List<AddressLiquidViewModel> Addresses { get; set; }
        public Guid? SelectedAddressId { get; set; }
    }
}
