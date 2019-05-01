using System.Collections.Generic;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Services.EntityScheme
{
    public class EntitySchemeListResponse: IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
        public List<EntitySchemeViewModel> EntitySchemes { get; set; }
        public long Count { get; set; }
    }
}