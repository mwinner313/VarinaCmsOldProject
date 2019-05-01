using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Common.MVC;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Form;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.Core.Web.ActionFilters;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Web.Controllers
{
    [AllowAnonymous]
    public class FormsController : LiquidController
    {
        private readonly IFormService _formService;
        public FormsController(IFormSchemeDataService formSchemeDataService,
            CustomFieldFactoryProvider<JObject> fieldFactoryPrvider, IFormService formService)
        {
            _formService = formService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MvcCmsFormDataActionFilter]
        public async Task<JsonResult> Submit(string formHandle)
        {
            var serviceRes = await _formService.SubmitNewForm(new FormSubmitRequest()
            {
                RequestOwner = User,
                Form = Request.Form,
                Files = Request.Files,
                FormSchemeHandle = formHandle
            });

            if (serviceRes.Access==ResponseAccess.Granted)
                return Json(new { success = true });

            if (serviceRes.Access==ResponseAccess.Deny) return Json(new { success = false, UnAuthorized=true });

            return Json(new { success = false });
        }
    }
}