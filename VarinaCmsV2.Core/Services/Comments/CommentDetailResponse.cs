using VarinaCmsV2.ViewModel.Comment;

namespace VarinaCmsV2.Core.Services.Comments
{
    public class CommentDetailResponse:IServiceResponse
    {
        public CommentDetailVeiwModel Comment { get; set; }
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
    }
}