using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.ViewModel.Eshop.Review
{
    public class ProductReviewHelpfulnessLiquidViewModel:BaseVeiwModel
    {
        public Guid ProductReviewId { get; set; }
        public bool WasHelpful { get; set; }
    }
}
