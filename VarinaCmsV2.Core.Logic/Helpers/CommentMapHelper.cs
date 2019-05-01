using System.Collections.Generic;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Comment;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class CommentMapHelper
    {
        public static Comment MapToModel(this CommentAddOrUpdateViewModel vm)
        {
            return Mapper.Map<Comment>(vm);
        }
        public static CommentViewModel MapToViewModel(this Comment vm)
        {
            return Mapper.Map<CommentViewModel>(vm);
        }
        public static CommentDetailVeiwModel MapToViewModelWithTarget(this Comment vm,Entity target)
        {
            var mapped = Mapper.Map<CommentDetailVeiwModel>(vm);
            mapped.Entity = target.MapToViewModel();
            return mapped;
        }
        public static CommentDetailVeiwModel MapToViewModelWithTarget(this Comment vm, Page target)
        {
            var mapped = Mapper.Map<CommentDetailVeiwModel>(vm);
            mapped.Page = target.MapForShowViewModel();
            return mapped;
        }
        public static List<CommentViewModel> MapToViewModel(this IEnumerable<Comment> vm)
        {
            return Mapper.Map<List<CommentViewModel>>(vm);
        }

        public static CommentWebClientViewModel MapToWebClinetViewModel(this Comment vm)
        {
            return Mapper.Map<CommentWebClientViewModel>(vm);
        }
    }
}
