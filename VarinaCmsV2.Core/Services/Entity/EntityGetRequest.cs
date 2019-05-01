using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Services.Entity
{
    public class EntityGetRequest
    {
        public Guid Id { get; set; }
        public string SchemeHandle { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}
