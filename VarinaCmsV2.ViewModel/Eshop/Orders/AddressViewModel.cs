using System;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.ViewModel.States;

namespace VarinaCmsV2.ViewModel.Eshop.Orders
{
    public class AddressViewModel
    {
        public string ReciverName { get; set; }
        public string Email { get; set; }
        public string Path { get; set; }
        public string MapLatLang { get; set; }
        public string ZipPostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public Guid StateProvinceId { get; set; }
        public StateProvinceViewModel StateProvince { get; set; }
    }
}