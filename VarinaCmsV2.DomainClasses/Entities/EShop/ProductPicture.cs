using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.DomainClasses.Entities.EShop
{
    public class ProductPicture:BaseEntity
    {
        public string Path { get; set; }
        public Guid ProductId { get; set; }
        public int DisplayOrder { get; set; }
        public string AltAttribute { get; set; }
        public string TitleAttribute { get; set; }
  
    }
}
