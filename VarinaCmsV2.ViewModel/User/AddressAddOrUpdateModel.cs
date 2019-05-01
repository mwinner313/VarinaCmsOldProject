using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.ViewModel.User
{
    public class AddressAddOrUpdateModel:BaseVeiwModel
    {
        
        [Required]
        public string ReciverName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public string ZipPostalCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public Guid StateProvinceId { get; set; }
        public string MapLatLang { get; set; }
    }
}
