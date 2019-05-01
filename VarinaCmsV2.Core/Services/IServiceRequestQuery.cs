using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Services
{
   public interface IServiceRequestQuery
    {
         string LanguageName { get; set; }
         int PageSize { get; set; } 
         int PageNumber { get; set; }
    }
}
