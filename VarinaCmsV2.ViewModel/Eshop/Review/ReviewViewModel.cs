using System;
using System.Collections.Generic;

namespace VarinaCmsV2.ViewModel.Eshop.Review
{
    public class ReviewViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public bool IsApproved { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public int HelpfulYesTotal { get; set; }
        public int HelpfulNoTotal { get; set; }
        public List<ProductReviewHelpfulnessViewModel> ProductReviewHelpfulnesses { get; set; }

    }
}
