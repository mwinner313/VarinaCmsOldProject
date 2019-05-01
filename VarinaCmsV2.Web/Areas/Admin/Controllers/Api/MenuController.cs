using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Menu;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.ViewModel.Menu;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    public class MenuController : BaseApiController
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
      
        [HttpGet]
        [WebApiCmsAuthorize(Premissions = MenuPremssion.CanSee)]
        public async Task<IHttpActionResult> Get()
        {
            var serviceRes = await _menuService.Get(new MenuListRequest()
            {
                RequestOwner = User
            });

            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(  serviceRes.Menus );
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
        [HttpPost]
        [WebApiCmsAuthorize(Premissions = MenuPremssion.CanAdd)]
        public async Task<IHttpActionResult> Post(MenuAddUpdateViewModel model)
        {
            var serviceRes = await _menuService.Add(new MenuAddRequest()
            {
                RequestOwner = User,
                ViewModel = model
            });
            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.Menu);
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }

        [WebApiCmsAuthorize(Premissions = MenuPremssion.CanUpdate)]
        [HttpPut]
        public async Task<IHttpActionResult> Put(Guid id, [FromBody]MenuAddUpdateViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(GetModelStateErorrs());
            var serviceRes = await _menuService.Update(new MenuUpdateRequest()
            {
                RequestOwner = User,
                ViewModel = model,
                MenuId = id
            });

            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.Menu);
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
        [WebApiCmsAuthorize(Premissions = MenuPremssion.CanDelete)]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var serviceRes = await _menuService.Delete(new MenuDeleteRequest()
            {
                RequestOwner = User,
                MenuId = id
            });
            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok();
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }


        [WebApiCmsAuthorize(Premissions = MenuPremssion.CanUpdate)]
        [Route("api/menu/addmenuitem")]
        [HttpPost]
        public async Task<IHttpActionResult> AddMenuItem([FromBody]List<MenuItemAddUpdateViewModel> model)
        {
            var menuItem = await _menuService.AddSingleMenuItem(model);
            return Ok(menuItem);
        }
    }
}
