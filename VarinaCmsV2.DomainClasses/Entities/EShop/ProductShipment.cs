using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VarinaCmsV2.DomainClasses.Entities.EShop
{
    [ComplexType]
    public class ProductShipment
    {
        public bool FreeShipping { get; set; }
        public bool IsShipEnabled { get; set; }
        public bool ShipSeparately { get; set; }
        public long SendPrice { get; set; }
        public long Weight { get; set; }
        public long Length { get; set; }
        public long Width { get; set; }
        public long Height { get; set; }
    
    }
}
