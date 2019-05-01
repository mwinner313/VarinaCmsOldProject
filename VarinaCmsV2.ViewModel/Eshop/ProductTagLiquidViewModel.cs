using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.ViewModel.Eshop
{
    public class ProductTagLiquidViewModel:PaginatedUrlableLiquidAdapter
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public List<ProductLiquidAdapter> Products { get; set; }
    }
}