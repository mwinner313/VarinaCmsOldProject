using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Web.Security.Mvc;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers
{
    public class PanelController : Controller
    {
       [MvcCmsAuthorize(Premissions = PanelPremission.Default)]
        public ActionResult Index()
        {
            return View();
        }
    }
}