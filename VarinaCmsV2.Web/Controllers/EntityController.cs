using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Common.MVC;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Contracts.WebClientServices;
using VarinaCmsV2.Core.Contracts.WebClientServices.Entities;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Web.Controllers
{
    [AllowAnonymous]
    public class EntityController : LiquidController
    {
        private readonly IRestrictedItemAccessManager _accessManager;
        private readonly IMetaDataDataService _metaData;
        private readonly ICategoryDataService _categoryDataService;
        private readonly IEntitySchemeWebClientService _entitySchemes;
        private readonly IEntityWebClientService _entityService;
        private readonly ITagDataService _tagDataService;
        private readonly IAppUserManager _userManager;

        public EntityController(IEntitySchemeWebClientService entitySchemes,
            CustomFieldFactoryProvider<JObject> customFieldFactoryProvider, IEntityWebClientService entityService,
            IRestrictedItemAccessManager accessManager, ITagDataService tagDataService,
            ICategoryDataService categoryDataService, IAppUserManager userManager,IMetaDataDataService metaData)
        {
            _metaData = metaData;
            _entityService = entityService;
            _accessManager = accessManager;
            _tagDataService = tagDataService;
            _categoryDataService = categoryDataService;
            _entitySchemes = entitySchemes;
            _userManager = userManager;
        }

        public async Task<ActionResult> ShowItemAsync(string entityUrl, string schemeUrl)
        {
            var entity = await _entityService.GetByUrlAndSchemeUrlAsync(entityUrl, schemeUrl);
            if (!_accessManager.HasAccess(entity, AccessPremission.See))
                return new HttpUnauthorizedResult();
            return LiquidView(entity.AsLiquidAdapted());
        }

        public async Task<ActionResult> ListByTagAsync(string tagUrl, string schemeUrl, int pageNumber = 1)
        {
            var tag = await _tagDataService.GetByUrlAndSchemeUrlAsync(tagUrl, schemeUrl);
            var entities = await _entityService.GetByTagAsync(tag, pageNumber);
            return LiquidView(tag.AsLiquidAdaptedWithEntities(entities));
        }

        public async Task<ActionResult> ListBySchemeAsync(string tagUrl, string schemeUrl, int pageNumber = 1)
        {
            var scheme = await _entitySchemes.GetByUrlAsync(tagUrl);
            var entities = await _entityService.GetBySchemeAsync(scheme, pageNumber);
            return LiquidView(scheme.AsLiquidAdaptedWithEntities(entities));
        }

        public async Task<ActionResult> ListByCategoryAsync(string[] categoryUrl, string schemeUrl, int pageNumber = 1)
        {
            var category =
                await _categoryDataService.GetByUrlAndSchemeUrlAsync(string.Join("/", categoryUrl), schemeUrl);
            var paginatedEntities = await _entityService.GetByCategoryAsync(category, pageNumber);
            return LiquidView(category.AsLiquidAdaptedWithEntitiesAndMetaData(paginatedEntities,_metaData));
        }

        public async Task<ActionResult> ShowUserCreatedEntitiesAsync(string userUrl, string schemeUrl, int pageNumber)
        {
            var user = await _userManager.FindByUrlAsync(userUrl);
            var userCreatedEntities = await _entityService.GetByUserAsync(user, schemeUrl, pageNumber);
            return LiquidView(userCreatedEntities.AsLiquidAdapted());
        }

        public async Task<ActionResult> SearchAsync(string schemeUrl, string searchText)
        {
            if (string.IsNullOrEmpty(searchText)) return LiquidNotFoundView();

            var scheme = await _entitySchemes.GetByUrlAsync(schemeUrl);
            var entities = await _entityService.SearchAsync(schemeUrl, searchText);
            return LiquidView(new EntitySearchResaultLiquidAdapted
            {
                SearchText = searchText,
                Entities = entities.AsLiquidAdapted(),
                Count = entities.Count,
                Scheme = scheme.AsLiquidAdapted()
            });
        }

        [AjaxRequest]
        [HttpGet]
        [Route("entity/like/{id:guid}")]
        public async Task<JsonResult> Like(Guid id)
        {
            var entityLikeCookie = new HttpCookie($"Entity-Like-{id}") {Expires = DateTime.Now.AddYears(10)};

            JsonResult response;

            if (Request.Cookies.AllKeys.ToList().All(x => x != entityLikeCookie.Name))
            {
                var entity = await _entityService.CountUpLike(id, User);
                Response.Cookies.Add(entityLikeCookie);
                response = Json(new {success = true, likesCount = entity.LikeCount}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var entity = await _entityService.CountDownLike(id, User);
                Response.Cookies[entityLikeCookie.Name].Expires = DateTime.Now.AddDays(-1);
                response = Json(new {success = true, likesCount = entity.LikeCount}, JsonRequestBehavior.AllowGet);
            }

            return response;
        }
    }
}