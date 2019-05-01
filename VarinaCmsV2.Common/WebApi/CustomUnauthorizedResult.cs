using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace VarinaCmsV2.Common.WebApi
{
    public class CustomUnauthorizedResult : UnauthorizedResult
    {
        private readonly string _message;
        public CustomUnauthorizedResult(IEnumerable<AuthenticationHeaderValue> challenges, HttpRequestMessage request, string message) : base(challenges, request)
        {
            _message = message;
        }

        public CustomUnauthorizedResult(IEnumerable<AuthenticationHeaderValue> challenges, ApiController controller, string message) : base(challenges, controller)
        {
            _message = message;
        }

        public override async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
           var res= await base.ExecuteAsync(cancellationToken);
            res.Content=new StringContent(_message);
            return res;
        }
    }
}
