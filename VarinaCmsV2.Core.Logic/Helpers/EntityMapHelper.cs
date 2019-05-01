using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.Core.Contracts.WebClientServices.Entities;
using VarinaCmsV2.Core.FrontEndOptions;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.DTO;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class EntityMapHelper
    {
        public static Entity MapToEntity(this EntityAddOrUpdateViewModel model)
        {
            return Mapper.Map<Entity>(model);
        }
        public static void MapToExisting(this EntityAddOrUpdateViewModel model, Entity entity)
        {
            Mapper.Map(model, entity);
        }

        public static UserEntitiesPaginated MapToModel(this UserEntitiesPaginatedLiquidAdapted model)
        {
            return Mapper.Map<UserEntitiesPaginated>(model);
        }
        public static UserEntitiesPaginatedLiquidAdapted AsLiquidAdapted(this UserEntitiesPaginated model)
        {
            return Mapper.Map<UserEntitiesPaginatedLiquidAdapted>(model);
        }

        public static EntityViewModel MapToViewModel(this Entity model)
        {
            return Mapper.Map<EntityViewModel>(model);
        }

        public static async Task<List<EntityViewModel>> ToViewModelListAsync(this IQueryable<Entity> model)
        {
            return Mapper.Map<List<EntityViewModel>>(await model.ToListAsync());
        }

        public static EntityLiquidAdapter AsLiquidAdapted(this Entity entity)
        {
            return Mapper.Map<EntityLiquidAdapter>(entity);
        }
        public static List<EntityLiquidAdapter> AsLiquidAdapted(this List<Entity> entities)
        {
            return Mapper.Map<List<EntityLiquidAdapter>>(entities);
        }

        public static IQueryable<Entity> PaginateByConfig(this IQueryable<Entity> query, int pageNumber)
        {

            return query.Skip((pageNumber - 1) * FrontEndDeveloperOptions.Instance.Pagination.Default)
                .Take(FrontEndDeveloperOptions.Instance.Pagination.Default);

        }

    }
}
