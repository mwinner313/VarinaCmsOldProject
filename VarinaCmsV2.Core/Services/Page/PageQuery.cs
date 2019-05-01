using VarinaCmsV2.Common;

namespace VarinaCmsV2.Core.Services.Page
{
    public class PageQuery:Pagenation, IServiceRequestQuery
    {
        public string SearchText { get; set; }
        public string OrderByField { get; set; }
        public string LanguageName { get; set; }
    }
}