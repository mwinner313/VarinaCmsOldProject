using System.Security.Principal;
using System.Threading.Tasks;
using StructureMap;
using StructureMap.Attributes;

namespace VarinaCmsV2.Core.Infrastructure
{
    public interface IBaseBeforeAddingEntityLogic
    {
        [SetterProperty]
         IContainer Container { get; set; }
          Task ApplyLogicAsync<T>(T obj,IPrincipal requestOwner) where T : class;
    }
}
