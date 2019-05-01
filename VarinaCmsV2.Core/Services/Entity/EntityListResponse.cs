using System.Collections.Generic;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Services.Entity
{
    public class EntityListResponse: IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public IList<EntityViewModel> Entities { get; set; }
        public int Count { get; set; }
        public string Message { get; set; } = "";
    }
}