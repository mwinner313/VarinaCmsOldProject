using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Web.Security.WebApi;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    [WebApiCmsAuthorize]
    public class LanguageController : ApiController
    {
        private readonly ILanguageDataService _languageDataService;

        public LanguageController(ILanguageDataService languageDataService)
        {
            _languageDataService = languageDataService;
        }

        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _languageDataService.Query.ToListAsync());
        }
    }
}
