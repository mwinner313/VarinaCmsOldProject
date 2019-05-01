using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.DomainClasses.Entities.EShop.Shipping
{
    public class ShippingMethod:BaseEntity,IVisibliltyControlled
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsVisible { get; set; }
    }
}
