using System;
using System.Security.Principal;
using VarinaCmsV2.ViewModel.Forms;

namespace VarinaCmsV2.Core.Services.FormScheme
{
    public class FormSchemeEditRequest:IServiceRequest
    {
        public FormSchemeAddOrUpdateViewModel Model { get; set; }
        public IPrincipal RequestOwner { get; set; }
        public Guid FormSchemeId { get; set; }
    }
}