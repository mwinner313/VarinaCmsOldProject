using System.Collections.Generic;
using VarinaCmsV2.ViewModel.Forms;

namespace VarinaCmsV2.Core.Services.Form
{
    public class FormListReponse:IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
        public List<FormViewModel> Models { get; set; }
        public long Count { get; set; }
    }
}