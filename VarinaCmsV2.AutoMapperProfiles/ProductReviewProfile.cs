using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.ViewModel.Eshop.Review;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class ProductReviewProfile :Profile
    {
        public ProductReviewProfile()
        {
            CreateMap<ReviewAddOrUpdateViewModel, ProductReview>();
            CreateMap<ProductReview, ReviewLiquidViewModel>();
            CreateMap<ProductReviewHelpfulness, ProductReviewHelpfulnessViewModel>();
            CreateMap<ProductReviewHelpfulness, ProductReviewHelpfulnessLiquidViewModel>();
        }
    }
}
