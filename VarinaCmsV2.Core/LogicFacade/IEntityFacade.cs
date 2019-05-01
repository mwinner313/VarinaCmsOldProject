using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.LogicFacade
{
    public interface  IEntityFacade
    {
        Task AddAsync(Entity model);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Entity model);
    }
}
