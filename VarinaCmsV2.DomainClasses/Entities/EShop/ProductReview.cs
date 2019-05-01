using System;
using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.DomainClasses.Entities.EShop
{
    public class ProductReview : BaseEntity
    {
        #region props

        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public bool IsApproved { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public int HelpfulYesTotal { get; set; }
        public int HelpfulNoTotal { get; set; }
        #endregion
        #region navigation
        public User User { get; set; }
        public Product Product { get; set; }

        public List<ProductReviewHelpfulness> ProductReviewHelpfulnesses { get; set; }
        #endregion
    }
}