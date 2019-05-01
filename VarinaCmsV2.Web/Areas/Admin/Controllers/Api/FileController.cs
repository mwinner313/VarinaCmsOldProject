using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.File;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.ViewModel.FileData;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    public class FileController : BaseApiController
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        [WebApiCmsAuthorize(Premissions = FilePremission.CanSee)]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var serviceRes = await _fileService.Get(new FileGetRequest()
            {
                RequestOwner = User,
                FileId = id
            });

            IHttpActionResult res = BadRequest(serviceRes.Message);

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.Model);
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
        [HttpGet]
        [WebApiCmsAuthorize(Premissions = FilePremission.CanSee)]
        public async Task<IHttpActionResult> Get([FromUri]FileQuery query)
        {
            var serviceRes = await _fileService.Get(new FileGetListRequest()
            {
                RequestOwner = User,
                Query = query
            });

            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.FilesData);
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
        [HttpPost]
        [WebApiCmsAuthorize(Premissions = FilePremission.CanAdd)]
        public async Task<IHttpActionResult> Post()
        {
            IHttpActionResult res = BadRequest();

            if (!Request.Content.IsMimeMultipartContent())
            {
                return res;
            }
            var serviceRes = await _fileService.Add(new FileAddRequest()
            {
                RequestOwner = User,
                File = Request.Content
            });

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.Model);

            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }

        [WebApiCmsAuthorize(Premissions = FilePremission.CanUpdate)]
        [HttpPut]
        public async Task<IHttpActionResult> Put([FromUri]Guid id, [FromBody]FileDataAddOrUpdateVeiwModel model)
        {
            if (!ModelState.IsValid) return BadRequest(this.GetModelStateErorrs());
            var serviceRes = await _fileService.Edit(new FileEditRequest()
            {
                RequestOwner = User,
                ViewModel = model,
                FileId = id
            });

            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.Model);
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
        [WebApiCmsAuthorize(Premissions = FilePremission.CanDelete)]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var serviceRes = await _fileService.Delete(new FileDeleteRequest()
            {
                RequestOwner = User,
                FileId = id
            });
            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok();
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
    }
}
