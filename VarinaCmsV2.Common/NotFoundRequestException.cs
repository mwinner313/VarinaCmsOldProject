using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VarinaCmsV2.Common
{
    public class NotFoundRequestException:Exception
    {
        private readonly IPrincipal _user;
        private readonly HttpRequestBase _request;
        public NotFoundRequestException(IPrincipal user, HttpRequestBase request)
        {
            _user = user;
           _request = request;
        }
    }
}
