using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace VarinaCmsV2.Common.WebApi
{
    public class BaseApiController: ApiController
    {

        protected IHttpActionResult Unauthorized(string messege)
        {
          
            var res = base.Unauthorized();
            return new CustomUnauthorizedResult(new List<AuthenticationHeaderValue>(), res.Request,messege);
        }
        protected string GetModelStateErorrs()
        {
            return string.Join(",",
                                    ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage)).ToList());
        }
    }
}
