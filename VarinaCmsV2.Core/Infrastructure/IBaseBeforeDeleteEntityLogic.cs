using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using StructureMap.Attributes;

namespace VarinaCmsV2.Core.Infrastructure
{
    public interface IBaseBeforeDeleteEntityLogic
    {
        [SetterProperty]
        IContainer Container { get; set; }
        Task ApplyLogicAsync<T>(T obj, IPrincipal requestOwner) where T : class;
    }
}
