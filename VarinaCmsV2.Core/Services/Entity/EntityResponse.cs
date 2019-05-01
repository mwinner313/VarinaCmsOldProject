using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Services.Entity
{
    public class EntityResponse: IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public EntityViewModel Entity { get; set; }
        public string Message { get; set; } = "";
    }
}
