using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Web.Exceptions
{
    public class NotDefinedCmsAuthorizeAttributeException:Exception
    {
        public NotDefinedCmsAuthorizeAttributeException(string actionName,string controllerName)
            :base($"CmsAuthrozie not defined on action : {actionName} in controller : {controllerName}")
        {
         
        }
    }
}
