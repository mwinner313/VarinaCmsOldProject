using System;

namespace VarinaCmsV2.DomainClasses.Entities.EShop
{
    public class ProductReviewHelpfulness:BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ProductReviewId { get; set; }
        public bool WasHelpful { get; set; }
        //public Portal Portal { get; set; }
        //public Guid PortalId { get; set; }
    }
}