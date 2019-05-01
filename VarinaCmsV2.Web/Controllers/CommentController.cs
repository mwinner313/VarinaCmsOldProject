using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.Core.Contracts.WebClientServices.Comment;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Services.Settings;
using VarinaCmsV2.Core.Settings;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.ViewModel.Comment;

namespace VarinaCmsV2.Web.Controllers
{
    [AllowAnonymous]
    public class CommentController : Controller
    {
        private readonly IEntityDataService _entityDataService;
        private readonly ISettingService _settingService;
        private readonly ICmsUrlBuilderFactory _urlBuilder;
        private readonly IIdentityManager _identityManager;
        private readonly IPageDataService _pageDataService;
        private readonly IRestrictedItemAccessManager _accessManager;
        private readonly ICommentWebClientService _commentService;
        public CommentController(
            ISettingService settingService,
            IPageDataService pageDataService,
            IEntityDataService entityDataService,
            IRestrictedItemAccessManager accessManager,
            ICommentWebClientService commentService,
            ICmsUrlBuilderFactory urlBuilder
            )
        {
            _entityDataService = entityDataService;
            _accessManager = accessManager;
            _settingService = settingService;
            _pageDataService = pageDataService;
            _commentService = commentService;
            _urlBuilder = urlBuilder;
        }
        [HttpPost]
        public async Task<ActionResult> AddEntityComment(CommentAddOrUpdateViewModel model)
        {
            var entity = await _entityDataService.Query.Include(x => x.Scheme).FirstOrDefaultAsync(x => x.Id == model.TargetId);
            if (entity == null) return HttpNotFound();
            if (!_accessManager.HasAccess(entity, AccessPremission.See)) return View("_UnAuthorized");


            if (!CanCommentBaseOnSitePolicies())
            {
                return View("_UnAuthorized");
            }

            if (entity.IsCommentDisabled) return View("Error");

            model.TargetType = CommentTargetType.Entity;
            var res = await _commentService.AddNewComment(model.MapToModel());
            if (Request.IsAjaxRequest())
            {
                return Json(new { success = true, comment = res.MapToWebClinetViewModel() });
            }
            return Redirect(_urlBuilder.GetUrlBuilder(entity).Generate(entity));
        }

        private bool CanCommentBaseOnSitePolicies()
        {
            var sitePolicy = _settingService.GetSetting<WebSitePolicies>();

            switch (sitePolicy.CommentPolicy)
            {
                case CommentPolicy.RegisteredUsers: return User.Identity.IsAuthenticated;
                case CommentPolicy.UsersInRoles:
                    return User.Identity.IsAuthenticated &&
                           _identityManager.CurrentIdentityHasOneOfRoles(
                               sitePolicy.CommentPolicyUserInRoles.Select(x => x.Name));
                default: return true;
            }

        }

        [HttpPost]
        public async Task<ActionResult> AddPageComment(CommentAddOrUpdateViewModel model)
        {
            var page = await _pageDataService.Query.FirstOrDefaultAsync(x => x.Id == model.TargetId);
            if (page == null) return HttpNotFound();
            if (!_accessManager.HasAccess(page, AccessPremission.See)) return View("_UnAuthorized");

            if (page.IsCommentDisabled) return View("Error");

            if (!CanCommentBaseOnSitePolicies())
            {
                return View("_UnAuthorized");
            }

            model.TargetType = CommentTargetType.Page;
            var res = await _commentService.AddNewComment(model.MapToModel());
            if (Request.IsAjaxRequest())
            {
                return Json(new { success = true, comment = res.MapToWebClinetViewModel() });
            }
            return Redirect(_urlBuilder.GetUrlBuilder(page).Generate(page));
        }
    }
}