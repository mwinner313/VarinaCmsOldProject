using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace VarinaCmsV2.Common.MVC
{
    public class CustomJsonResult:JsonResult
    {

        public int StatusCode { get; set; } = 200;
        public override void ExecuteResult(ControllerContext context)
        {

            base.ExecuteResult(context);
            context.HttpContext.Response.StatusCode = StatusCode;
        }
    }
}
