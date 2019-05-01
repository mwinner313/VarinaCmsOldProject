using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Common
{
    public class Pagenation
    {
        public int PageSize { get; set; } = 15;
        public int PageNumber { get; set; } = 1;
        public int SkipSize => PageSize * (PageNumber-1);
        public bool NoPaginate { get; set; }

    }
}
