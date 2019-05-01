using System;
using System.ComponentModel.DataAnnotations.Schema;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.Users
{
    public class Address:BaseEntity
    {
        public string ReciverName { get; set; }
        public string Email { get; set; }
        public string Path { get; set; }
        public string MapLatLang { get; set; }
        public string ZipPostalCode { get; set; }
        public string PhoneNumber { get; set; }
        [ForeignKey(nameof(StateProvince))]
        public Guid StateProvinceId { get; set; }
        public StateProvince StateProvince { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}