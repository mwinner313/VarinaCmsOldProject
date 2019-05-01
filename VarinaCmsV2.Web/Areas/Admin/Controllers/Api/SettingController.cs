using System.Threading.Tasks;
using System.Web.Http;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services.Settings;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    [WebApiCmsAuthorize(Premissions = PanelPremission.Default)]

    public class SettingController : BaseApiController
    {
        private readonly ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [WebApiCmsAuthorize(Roles = PreDefRoles.PrincipalAdministrator + "," + PreDefRoles.Supervisor + "," + PreDefRoles.Administrator)]
        public async Task<IHttpActionResult> Get(string id)
        {
            return Ok(_settingService.GetSetting(id));
        }
        [HttpGet]
        [WebApiCmsAuthorize(Roles = PreDefRoles.PrincipalAdministrator + "," + PreDefRoles.Supervisor + "," + PreDefRoles.Administrator)]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(_settingService.GetAllSettings());
        }
        [HttpPut]
        [WebApiCmsAuthorize(Roles = PreDefRoles.PrincipalAdministrator + "," + PreDefRoles.Supervisor + "," + PreDefRoles.Administrator)]
        public async Task<IHttpActionResult> Put(Setting setting)
        {
            await _settingService.SaveSetting(setting);
            return Ok();
        }
    }
}