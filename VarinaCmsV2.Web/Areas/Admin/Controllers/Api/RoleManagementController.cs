using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services.RoleManagement;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    [WebApiCmsAuthorize(Premissions = PanelPremission.Default)]
    public class RoleManagementController : BaseApiController
    {
        private readonly IAppRoleManager _roleManager;

        public RoleManagementController(IAppRoleManager roleManager)
        {
            _roleManager = roleManager;
        }
        [WebApiCmsAuthorize(Roles = PreDefRoles.PrincipalAdministrator + "," + PreDefRoles.Administrator)]
        public async Task<IHttpActionResult> Get([FromUri]RoleQuery query)
        {
            query = query ?? new RoleQuery();
            if (query.JustNames)
            {
                return Ok(await _roleManager.Roles.Where(x => !x.IsSystematic).Select(x => x.Name).ToListAsync());
            }
            return Ok(await _roleManager.Roles.Where(x => !x.IsSystematic).ToListAsync());
        }
    }
}
