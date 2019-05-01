using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Services.Comments
{
    public interface ICommentService
    {
        Task<CommentListResponse> GetList(CommentGetListRequest request);
        Task<CommentApproveRespnose> Approve(CommentApproveRequest request);
        Task<CommentDeleteResponse> Delete(CommentDeleteRequest request);
        Task<CommentEditResponse> Edit(CommentEditRequest request);
        Task<CommentDetailResponse> GetDetial(CommentDetailRequest request);
    }
}