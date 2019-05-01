using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.EntityStates;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.LogicFacade
{
    public interface IEntitySchemeFacade
    {
        Task AddNewSchemeToSystemAsync(EntitySchemeViewModel model);
        Task UpdateSchemeAsync(EntitySchemeUpdateViewModel model);
        Task DeleteSchemeAsync(Guid id);
        Task<EntitySchemeViewModel> GetAsync(Guid id);
        Task<List<EntitySchemeViewModel>> GetAsync();
        Task<EntitySchemeState> GetEntitySchemeStateAsync(Guid id);
    }
}
