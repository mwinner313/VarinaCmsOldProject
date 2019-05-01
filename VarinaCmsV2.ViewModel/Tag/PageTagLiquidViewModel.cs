using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.Page;

namespace VarinaCmsV2.ViewModel.Tag
{
    public class PageTagLiquidViewModel: PaginatedUrlableLiquidAdapter
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public List<PageLiquidViewModel> Pages { get; set; }
    }
}
