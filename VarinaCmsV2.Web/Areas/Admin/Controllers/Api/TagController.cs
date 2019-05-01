using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
using AutoMapper.QueryableExtensions;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.ViewModel;
using VarinaCmsV2.Web.Areas.Admin.Models;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    [WebApiCmsAuthorize(Premissions = PanelPremission.Default)]
    public class TagController:BaseApiController
    {
        private readonly ITagDataService _tagDataService;

        public TagController(ITagDataService tagDataService)
        {
            _tagDataService = tagDataService;
        }

        public async Task<IHttpActionResult> Get([FromUri]TagQuery query)
        {
            var tags = _tagDataService.Query;
            if (!string.IsNullOrEmpty(query.SearchText)) tags = tags.Where(x => x.Title.Contains(query.SearchText));
            tags =tags.Where(x => x.SchemeId == query.SchemeId);
            return Ok(((await tags.OrderByDescending(x => x.Title).Take(18).ToListAsync()).MapToViewModel()));
        }
    }
}