using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using StructureMap;
using StructureMap.Attributes;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Security;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Security;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Web.Security.WebApi
{
    public class WebApiCmsAuthorize : AuthorizeAttribute
    {
        private readonly IIdentityManager _identityManager;
        private readonly ISecurityLogger _securityLogger;

        public WebApiCmsAuthorize()
        {
            _securityLogger = GetContainer().GetInstance<ISecurityLogger>();
            _identityManager = GetContainer().GetInstance<IIdentityManager>();
        }
        public static Func<IContainer> GetContainer { get; set; }
        public string Premissions { get; set; }
       
        
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.ControllerContext.RequestContext.Principal.IsInRole(PreDefRoles.Supervisor)) return;
            base.OnAuthorization(actionContext);
            HandleAuthorization(actionContext);
        }

        public override async Task OnAuthorizationAsync(HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
           if(actionContext.ControllerContext.RequestContext.Principal.IsInRole(PreDefRoles.Supervisor))return;
             await base.OnAuthorizationAsync(actionContext, cancellationToken);
            HandleAuthorization(actionContext);
        }
        private void HandleAuthorization(HttpActionContext actionContext)
        {
             
            if(string.IsNullOrEmpty(Premissions))return;

            if (!actionContext.ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                HandleUnauthorizedRequest(actionContext);
                return;
            }

            if (_identityManager.HasPremission(actionContext.ControllerContext.RequestContext.Principal,Premissions)) return;
            actionContext.Response=new HttpResponseMessage(HttpStatusCode.Forbidden);
        }
    }
}