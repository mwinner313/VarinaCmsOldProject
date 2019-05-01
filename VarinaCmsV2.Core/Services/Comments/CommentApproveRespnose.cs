namespace VarinaCmsV2.Core.Services.Comments
{
    public class CommentApproveRespnose:IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
    }
}