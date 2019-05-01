using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.DomainClasses.Contracts
{
    public abstract class PaginatedUrlableLiquidAdapter: UrlableLiquidAdapter
    {
        public int AllPagesCount { get; set; }

        public int CurrentPage { get; set; }
        public int FivePreviewsPage
        {
            get
            {
                if (CurrentPage - 5 < 1) return 1;
                return CurrentPage - 5;
            }
        }
        public int FiveNextPage
        {
            get
            {
                if (CurrentPage + 5 > AllPagesCount) return AllPagesCount;
                return CurrentPage + 5;
            }
        }
    }
}
