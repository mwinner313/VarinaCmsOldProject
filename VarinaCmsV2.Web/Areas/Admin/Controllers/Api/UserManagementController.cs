using System;
using System.Threading.Tasks;
using System.Web.Http;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.UserManagement;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    [WebApiCmsAuthorize(Roles = PreDefRoles.PrincipalAdministrator+","+PreDefRoles.Administrator)]
    public class UserManagementController : BaseApiController
    {
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [WebApiCmsAuthorize(Premissions = UserManagePremission.CanSeeUsers)]
        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri] UserQuery query)
        {
            var serviceRes = await _userManagementService.GetAsync(new UserListRequest()
            {
                Query = query,
                RequestOwner = User
            });

            IHttpActionResult res = BadRequest(serviceRes.Message);

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(new {items = serviceRes.Users, count = serviceRes.Count});
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }

        [WebApiCmsAuthorize(Premissions = UserManagePremission.CanSeeUsers)]
        [HttpGet]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var serviceRes = await _userManagementService.GetAsync(new UserGetRequest()
            {
                UserId = id,
                RequestOwner = User
            });

            IHttpActionResult res = BadRequest(serviceRes.Message);

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.User);
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }

        [WebApiCmsAuthorize(Premissions = UserManagePremission.CanAdd)]
        [HttpPost]
        public async Task<IHttpActionResult> Post(UserAddOrUpdateViewModel model)
        {
            var serviceRes = await _userManagementService.AddAsync(new UserAddRequest()
            {
                RequestOwner = User,
                User = model
            });

            IHttpActionResult res = BadRequest(serviceRes.Message);

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.User);
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }

        [WebApiCmsAuthorize(Premissions = UserManagePremission.CanUpdate)]
        [HttpPut]
        public async Task<IHttpActionResult> Put(Guid id, UserAddOrUpdateViewModel model)
        {
            if (id != model.Id) return BadRequest();
            var serviceRes = await _userManagementService.UpdateAsync(new UserUpdateRequest()
            {
                RequestOwner = User,
                User = model,UserId = id
            });

            IHttpActionResult res = BadRequest(serviceRes.Message);

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.User);
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }

        [WebApiCmsAuthorize(Premissions = UserManagePremission.CanDelete)]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var serviceRes = await _userManagementService.DeleteAsync(new UserDeleteRequest()
            {
                RequestOwner = User,
                UserId = id
            });

            IHttpActionResult res = BadRequest(serviceRes.Message);

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.User);
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
    }
}