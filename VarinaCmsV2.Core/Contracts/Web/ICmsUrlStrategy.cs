using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Contracts.Web
{
    public interface ICmsUrlBuilder
    {
        new string Generate(IUrlable item);
        new string GenerateFull(IUrlable item);
    }
}
