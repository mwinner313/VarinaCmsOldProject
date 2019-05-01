using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.Builders;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.EntityStates;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.LogicFacade;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Logic.Facades
{
    public class EntitySchemeFacade : IEntitySchemeFacade
    {
        private readonly IEntitySchemeDataService _schemesDataSrv;
        private readonly IEntityDataService _entitiesDataSrv;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISchemeBuilder<EntityScheme> _schemeBuilder;
        public EntitySchemeFacade(CustomFieldFactoryProvider<JObject> customFieldFactoryProvider,
            IEntitySchemeDataService schemesDataSrv,
            IUnitOfWork unitOfWork, ISchemeBuilder<EntityScheme> schemeBuilder, IEntityDataService entitiesDataSrv, ILanguageDataService languageDataService, CustomFieldFactoryProvider<JObject> customFieldFactoryProvider1, IFieldDefenitionDataService fieldDefenitionDataService)
        {
            _schemesDataSrv = schemesDataSrv;
            _unitOfWork = unitOfWork;
            _schemeBuilder = schemeBuilder;
            _entitiesDataSrv = entitiesDataSrv;
        }

        public async Task AddNewSchemeToSystemAsync(EntitySchemeViewModel model)
        {
            _schemeBuilder.SetBuildingScheme(model.MapToEntity());
            _schemeBuilder.AddFieldDefenition(model.FieldDefenitions.MapToModel());
            _schemeBuilder.AddFieldDefenitionGroup(AutoMapper.Mapper.Map<List<FieldDefenitionGroup>>(model.FieldDefenitionGroups));
            var newScheme = _schemeBuilder.GetResult();
            await _schemesDataSrv.AddAsync(newScheme);
        }
        public async Task UpdateSchemeAsync(EntitySchemeUpdateViewModel viewModel)
        {
            var existing = await _schemesDataSrv.GetAsync(viewModel.Id);
            await _schemesDataSrv.UpdateAsync(viewModel.MapToExisting(existing));
        }

        public async Task DeleteSchemeAsync(Guid id)
        {
            if((await GetEntitySchemeStateAsync(id)).CanDelete)
                throw new InvalidOperationException("there is entities with this scheme in the system");
            await _schemesDataSrv.DeleteAsync(id);
        }

        public async Task<EntitySchemeViewModel> GetAsync(Guid id)
        {
            var scheme = await _schemesDataSrv.GetAsync(id);
            return scheme.MapToViewModel();
        }

        public Task<List<EntitySchemeViewModel>> GetAsync()
        {
            return _schemesDataSrv.Query.ProjectTo<EntitySchemeViewModel>().ToListAsync();
        }

        public async Task<EntitySchemeState> GetEntitySchemeStateAsync(Guid id)
        {
         return new EntitySchemeState()
         {
            EntitiesCount =  await _entitiesDataSrv.Query.CountAsync(x => x.SchemeId == id)
         };
        }
    }
}
