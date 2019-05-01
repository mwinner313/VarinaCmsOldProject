using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.Users
{
    public class StateProvince:BaseEntity,IVisibliltyControlled
    {
        public string Name { get; set; }
        public bool IsVisible { get; set; }

    }
}