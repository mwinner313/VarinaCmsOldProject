using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.WebHost;

namespace VarinaCmsV2.Common.WebApi
{
    public class NoBufferMoreThan4MbPolicySelector : WebHostBufferPolicySelector
    {
        public NoBufferMoreThan4MbPolicySelector()
        {
        }
        public override bool UseBufferedInputStream(object hostContext)
        {
            var context = hostContext as HttpContextBase;
            if (context?.Request.ContentLength > 4194304)//4mb
                return false;

            return true;
        }

    }
}
