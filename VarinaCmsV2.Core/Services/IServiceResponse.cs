using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Services
{
   public interface IServiceResponse
    {
         ResponseAccess Access { get; set; }
         string Message { get; set; }
    }
}
