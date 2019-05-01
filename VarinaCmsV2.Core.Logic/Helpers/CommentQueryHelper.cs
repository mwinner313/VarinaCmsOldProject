using System.Linq;
using VarinaCmsV2.Core.Services.Comments;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class CommentQueryHelper
    {
        public static IQueryable<Comment> Filter(this IQueryable<Comment> comments, CommentQuery query)
        {
            return comments.FilterByStatus(query.Status).FilterBySearchText(query.SearchText);
        }
        private static IQueryable<Comment> FilterByStatus(this IQueryable<Comment> comments, CommentStatus status)
        {
            switch (status)
            {
                case CommentStatus.All: return comments;
                case CommentStatus.Approved: return comments.Where(x => x.Approved);
                case CommentStatus.NotApproved: return comments.Where(x => !x.Approved);
                default: return comments;
            }
        }
        private static IQueryable<Comment> FilterBySearchText(this IQueryable<Comment> comments, string searchText)
        {
            return string.IsNullOrEmpty(searchText) ? comments : comments.Where(x => x.Text.Contains(searchText));
        }
    }
}
