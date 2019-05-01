using System.Collections.Generic;

namespace VarinaCmsV2.Core.FrontEndOptions
{
    public class PaginationOption
    {
        public int Default { get; set; }
        public int HomePage { get; set; }

        /// <summary>
        /// used for categories to override default pagenation
        /// </summary>
        public List<CustomPagination> CustomPaginations { get; set; }
    }
}