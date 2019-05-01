using System.Collections.Specialized;
using System.Security.Principal;
using System.Web;

namespace VarinaCmsV2.Core.Services.Form
{
    public class FormSubmitRequest:IServiceRequest
    {
        public string FormSchemeHandle { get; set; }
        public NameValueCollection Form { get; set; }
        public HttpFileCollectionBase Files { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}