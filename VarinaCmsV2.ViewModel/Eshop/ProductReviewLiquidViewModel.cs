using System;
using DotLiquid;

namespace VarinaCmsV2.ViewModel.Eshop
{
    public class ProductReviewLiquidViewModel:Drop
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public bool IsApproved { get; set; }
        public string Title { get; set; }
        public int Rating { get; set; }
        public int HelpfulYesTotal { get; set; }
        public int HelpfulNoTotal { get; set; }
    }
}