using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Entities.Enums;

namespace VarinaCmsV2.Core.Services.Comments
{
    public class CommentQuery : Pagenation
    {
        public CommentStatus Status { get; set; }
        public string SearchText { get; set; }
    }
}