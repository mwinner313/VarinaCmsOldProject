using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.ViewModel.Eshop
{
    public class ProductsCompareModel
    {
        public ProductCategory PrimaryCategory { get; set; }
        public EntityScheme Scheme { get; set; }
        public List<Product> Products { get; set; }
    }
}
