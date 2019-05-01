using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.ViewModel.Eshop
{
    public class SearchProductResultLiquidViewModel:LiquidAdapter
    {
        public List<ProductLiquidAdapter> Products { get; set; }
        public string SearchText { get; set; }
        public long Count { get; set; }
    }
}
