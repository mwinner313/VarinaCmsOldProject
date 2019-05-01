using VarinaCmsV2.ViewModel.Forms;

namespace VarinaCmsV2.Core.Services.FormScheme
{
    public class FormSchemeResponse : IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
        public FormSchemeViewModel Model { get; set; }
    }
}