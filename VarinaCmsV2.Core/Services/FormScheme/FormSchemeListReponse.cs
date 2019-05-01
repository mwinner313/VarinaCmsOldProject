using System.Collections.Generic;
using System.Threading.Tasks;
using VarinaCmsV2.ViewModel.Forms;

namespace VarinaCmsV2.Core.Services.FormScheme
{
    public class FormSchemeListReponse : IServiceResponse
    {
        public List<FormSchemeViewModel> Models { get; set; }
        public ResponseAccess Access { get; set; }
        public long Count { get; set; }
        public string Message { get; set; }
    }
}