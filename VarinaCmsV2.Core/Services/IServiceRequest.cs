using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Services
{
    public interface IServiceRequest
    {
         IPrincipal RequestOwner { get; set; }
    }
}
