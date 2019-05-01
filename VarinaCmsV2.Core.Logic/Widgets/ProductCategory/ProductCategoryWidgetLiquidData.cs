using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.Widget;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Core.Logic.Widgets.ProductCategory
{
    public class ProductCategoryWidgetLiquidData : BaseWidgetLiquidData
    {
        public ProductCategoryLiquidVeiwModel ProductCategory { get; set; }
    }
}
