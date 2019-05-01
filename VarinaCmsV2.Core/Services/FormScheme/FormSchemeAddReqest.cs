using System.Security.Principal;
using VarinaCmsV2.ViewModel.Forms;

namespace VarinaCmsV2.Core.Services.FormScheme
{
    public class FormSchemeAddReqest:IServiceRequest
    {
        public FormSchemeAddOrUpdateViewModel Model { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}