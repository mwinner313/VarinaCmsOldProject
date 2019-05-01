using AutoMapper;

using VarinaCmsV2.Core.EntityStates;
using VarinaCmsV2.Core.LogicFacade;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.Page;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class EntitySchemeProfile:AutoMapper.Profile
    {
        public EntitySchemeProfile()
        {
            CreateMap<EntitySchemeViewModel, EntityScheme>();
            CreateMap<EntitySchemeUpdateViewModel, EntityScheme>();
            CreateMap<EntitySchemeState, EntitySchemeStateViewModel>();
            CreateMap<EntitySchemeStateViewModel, EntitySchemeState > ();
            CreateMap<EntityScheme, EntitySchemeViewModel>();
            CreateMap<EntityScheme, EntitySchemeLiquidViewModel>();
            CreateMap<EntitySchemeLiquidViewModel, EntityScheme>();
        }
    }

    public class EntitySchmeStateResolver : IValueResolver<EntityScheme, EntitySchemeViewModel, EntitySchemeStateViewModel>
    {
        private readonly IEntitySchemeFacade _entitySchemeFacade;

        public EntitySchmeStateResolver(IEntitySchemeFacade entitySchemeFacade)
        {
            _entitySchemeFacade = entitySchemeFacade;
        }

        public EntitySchemeStateViewModel Resolve(EntityScheme source, EntitySchemeViewModel destination,
            EntitySchemeStateViewModel destMember, ResolutionContext context)
        {
            var state = _entitySchemeFacade.GetEntitySchemeStateAsync(source.Id);
            return Mapper.Map<EntitySchemeStateViewModel>(state);
        }
    }
}
