using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DotLiquid;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.Core.Logic.Decorators;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.Core.Web.ActionFilters;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel;

namespace VarinaCmsV2.Web.Controllers
{
    [AllowAnonymous]

    public class HomeController : LiquidController
    {
        public async Task<ActionResult> Index()
        {
            return LiquidHomePageView(new HomePageDataLiquidViewModel());
        }

        public async Task<ActionResult> NotFound()
        {
            
            Elmah.ErrorSignal.FromCurrentContext().Raise(new NotFoundRequestException(User,Request));

            return LiquidNotFoundView();
        }
    }
}