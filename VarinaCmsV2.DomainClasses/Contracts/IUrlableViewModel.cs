using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.DomainClasses.Contracts
{
    public interface IUrlableViewModel
    {
         string ToUrl { get; set; }
         string ToFullUrl { get; set; }
    }
}
