using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.Comment;
using VarinaCmsV2.ViewModel.Tag;
using VarinaCmsV2.ViewModel.User.Account;

namespace VarinaCmsV2.ViewModel.Page
{
    public class PageLiquidViewModel:UrlableLiquidAdapter
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public LiquidDateTime UpdateDateTime { get; set; }
        public LiquidDateTime PublishDateTime { get; set; }
        public string HtmlContent { get; set; }
        public  List<PageTagLiquidViewModel> Tags { get; set; }
        public UserLiquidViewModel Creator { get; set; }
        public List<CommentLiquidViewModel> Comments { get; set; }
    }
}
