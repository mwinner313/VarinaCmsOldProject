using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.ViewModel.Eshop
{
    public class ProductsCompareModelLiquidVeiwModel:LiquidAdapter
    {
        public List<ProductLiquidAdapter> Products { get; set; }
        public EntitySchemeLiquidViewModel Scheme { get; set; }
        public ProductCategoryLiquidVeiwModel PrimaryCategory { get; set; }
    }
}
