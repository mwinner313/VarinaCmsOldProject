using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.Web.Areas.Admin.Models;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api.Validation
{
    [WebApiCmsAuthorize(Premissions = PanelPremission.Default)]
    public class PageValidatorController : BaseApiController
    {
        private readonly IPageDataService _dataService;

        public PageValidatorController(IPageDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IHttpActionResult> ExistsTitle(PageValidationContext context)
        {
            if (context.Id.HasValue && await _dataService.Query.AnyAsync(x =>
                    x.Title == context.Value && x.Id == context.Id))
                return Ok(new
                {
                    value = context,
                    isValid = true
                });

            return Ok(new
            {
                value = context,
                isValid = await _dataService.Query.AllAsync(x => x.Title != context.Value)
            });
        }
    }
}
