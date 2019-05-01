using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Comments;
using VarinaCmsV2.Core.Web.Security.WebApi;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    [RoutePrefix("api/comment")]
    public class CommentController : BaseApiController
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [Route()]
        [WebApiCmsAuthorize(Premissions = CommentPrmission.CanSee)]
        public async Task<IHttpActionResult> Get([FromUri]CommentQuery query)
        {
            IHttpActionResult res = BadRequest();
            var serviceRes = await _commentService.GetList(new CommentGetListRequest()
            {
                Query = query ?? new CommentQuery()
            });

            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes);

            return res;
        }

        [Route("{id:guid}")]
        [WebApiCmsAuthorize(Premissions = CommentPrmission.CanSee)]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            IHttpActionResult res = BadRequest();
            var serviceRes = await _commentService.GetDetial(new CommentDetailRequest()
            {
                Id=id,
                RequestOwner=User
            });

            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes);

            return res;
        }

        [Route("DeleteBatch")]
        [HttpPost]
        [WebApiCmsAuthorize(Premissions = CommentPrmission.CanDelete)]
        public async Task<IHttpActionResult> Delete(List<Guid> ids)
        {
            IHttpActionResult res = BadRequest();
            var serviceRes = await _commentService.Delete(new CommentDeleteRequest()
            {
                RequestOwner = User,
                Ids = ids
            });

            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes);

            return res;
        }


        [Route("ChangeApproveState")]
        [HttpPost]
        [WebApiCmsAuthorize(Premissions = CommentPrmission.CanChangeApproveState)]
        public async Task<IHttpActionResult> ChangeApproveState(List<Guid> ids)
        {
            IHttpActionResult res = BadRequest();
            var serviceRes = await _commentService.Approve(new CommentApproveRequest()
            {
                Ids = ids,
                RequestOwner = User
            });

            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes);

            return res;
        }
    }

}
