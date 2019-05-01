using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.Builders;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.LogicFacade;
using VarinaCmsV2.Core.Services.Category;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Logic.Facades
{
    public class EntityFacade : IEntityFacade
    {
        private readonly IEntityDataService _entityDataSrv;
        private readonly IEntitySchemeDataService _schemesDataSrv;
        private readonly IEntityBuilder<Entity,Category> _entityBuilder;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryDataService _categoryDataService;

        public EntityFacade(IEntityDataService entityDataSrv, IEntityBuilder<Entity,Category> entityBuilder, IEntitySchemeDataService schemesDataSrvcade, IUnitOfWork unitOfWork, ICategoryDataService categoryDataService)
        {
            _entityDataSrv = entityDataSrv;
            _entityBuilder = entityBuilder;
            _schemesDataSrv = schemesDataSrvcade;
            _unitOfWork = unitOfWork;
            _categoryDataService = categoryDataService;
        }

        public async Task AddAsync(Entity model)
        {
            await HandleAddition(model);
        }

        private async Task HandleAddition(Entity model)
        {
            var scheme = await _schemesDataSrv.GetAsync(model.SchemeId);
            var primaryCat =
                await _categoryDataService.Query.Include(x => x.FieldDefenitions)
                    .Include(x => x.FieldDefenitionGroups)
                    .Include(x => x.FieldDefenitionGroups.Select(fdg => fdg.FieldDefenitions))
                    .FirstAsync(x => x.Id == model.PrimaryCategoryId);
            _entityBuilder.SetBuildingEntity(model, scheme, primaryCat);

            var res = _entityBuilder.GetResult();

            await _entityDataSrv.AddAsync(res);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _entityDataSrv.DeleteAsync(id);
        }

        public async Task UpdateAsync(Entity model)
        {
            var cat =
                    _categoryDataService.Query
                    .Include(x => x.FieldDefenitions)
                    .Include(x => x.FieldDefenitionGroups)
                    .Include(x => x.FieldDefenitionGroups.Select(fdg=> fdg.FieldDefenitions))
                        .First(x => x.Id == model.PrimaryCategoryId);
            _entityBuilder.SetBuildingEntity(model, model.Scheme, cat);
            var res = _entityBuilder.GetResult();
            await _entityDataSrv.UpdateAsync(res);
        }


    }
}
