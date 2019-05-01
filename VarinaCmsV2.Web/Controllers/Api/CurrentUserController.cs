using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Services.CurrentUser;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.ViewModel.User.Account;

namespace VarinaCmsV2.Web.Controllers.Api
{
    [WebApiCmsAuthorize]
    public class CurrentUserController : BaseApiController
    {
        private readonly IAppUserManager _userManager;
        private readonly ICurrentUserService _currentUserService;
        public CurrentUserController(IAppUserManager userManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(Mapper.Map<CurrentUserViewModel>(await _userManager.GetCurrentUserAsync()));
        }
        [HttpPut]
        public async Task<IHttpActionResult> Put(EditCurrentUserModel model)
        {
            model.UserAvatar = Request.Content;
            await _currentUserService.Edit(model);
            return Ok();
        }
        //usedfor sending image
        [HttpPost]
        [FileUpload]
        public async Task<IHttpActionResult> Post()
        {
            IHttpActionResult res = BadRequest();

            await _currentUserService.Edit(new EditCurrentUserModel() { UserAvatar = Request.Content, Action = UserEditAction.ChangeAvatar });
            return Ok(_userManager.GetCurrentUser().MapToViewModel());
        }
    }
}