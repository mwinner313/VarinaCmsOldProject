using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Services.EntityScheme
{
    public class EntitySchemeResponse:IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; } = "";
        public EntitySchemeViewModel EntityScheme { get; set; }
    }
}