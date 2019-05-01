using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid;

namespace VarinaCmsV2.ViewModel.User
{
    public class AddressLiquidViewModel:Drop
    {
        public Guid Id { get; set; }
        public string ReciverName { get; set; }
        public string Email { get; set; }
        public string Path { get; set; }
        public string ZipPostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public Guid ProvinceId { get; set; }
    }
}
