using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.ViewModel.Eshop.Review;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class ProductReviewMapHelper
    {
        public static ProductReview MapToModel(this ReviewAddOrUpdateViewModel vm)
        {
            return AutoMapper.Mapper.Map<ProductReview>(vm);
        }
    }
}
