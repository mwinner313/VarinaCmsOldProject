using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Comment;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentLiquidViewModel>();
            CreateMap<Comment, CommentViewModel>();
            CreateMap<Comment, CommentDetailVeiwModel>();
            CreateMap<Comment, CommentWebClientViewModel>();
            CreateMap<CommentAddOrUpdateViewModel, Comment>();
        }
    }
}
