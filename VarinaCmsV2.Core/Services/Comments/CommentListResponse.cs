using System.Collections.Generic;
using VarinaCmsV2.ViewModel.Comment;

namespace VarinaCmsV2.Core.Services.Comments
{
    public class CommentListResponse : IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
        public List<CommentViewModel> Items { get; set; }
        public int Count { get; set; }
    }
}