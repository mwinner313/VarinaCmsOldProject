using System;
using System.Collections.ObjectModel;
using System.Runtime.Remoting.Messaging;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.AutoMapperProfiles.Reslovers;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;
using System.Collections.Generic;
using VarinaCmsV2.Core.Contracts.DataServices;
using System.Linq;
using VarinaCmsV2.Core.Contracts.WebClientServices;
using VarinaCmsV2.Core.Contracts.WebClientServices.Entities;
using VarinaCmsV2.ViewModel.DTO;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<Entity, EntityViewModel>();
            CreateMap<EntityViewModel, Entity>();
            CreateMap<EntityAddOrUpdateViewModel, Entity>().ForMember(x => x.Fields, opt => opt.Ignore());
            CreateMap<Entity, EntityAddOrUpdateViewModel>();
            CreateMap<Entity, EntityLiquidAdapter>()
                .ForMember(x => x.Metas, opt => opt.ResolveUsing<EnitiyMetaDataHandler>())
                .ForMember(x => x.Entity, opt => opt.MapFrom(x => x))
                .ForMember(x => x.FieldFactoryProvider, opt => opt.ResolveUsing<FieldFactoryProviderResolver>());
            CreateMap<EntityLiquidAdapter, Entity>();
            CreateMap<UserEntitiesPaginated, UserEntitiesPaginatedLiquidAdapted>();
            CreateMap<UserEntitiesPaginatedLiquidAdapted, UserEntitiesPaginated>();

        }


    }
    public class EnitiyMetaDataHandler : IValueResolver<Entity, EntityLiquidAdapter, List<Meta>>
    {
        private readonly IMetaDataDataService _metas;
        public EnitiyMetaDataHandler(IMetaDataDataService metas)
        {
            _metas = metas;
        }

        public List<Meta> Resolve(Entity source, EntityLiquidAdapter destination, List<Meta> destMember, ResolutionContext context)
        {
            return _metas.Query.Where(x => x.TargetType == Entity.MetaType && x.TargetId == source.Id).ToList();
        }
    }
    public class FieldFactoryProviderResolver : IValueResolver<Entity, EntityLiquidAdapter, CustomFieldFactoryProvider<JObject>>
    {
        private readonly CustomFieldFactoryProvider<JObject> _customFieldFactoryProvider;

        public FieldFactoryProviderResolver()
        {
            _customFieldFactoryProvider = AutomapperConfiguration.Resolver.GetInstance<CustomFieldFactoryProvider<JObject>>();
        }

        public CustomFieldFactoryProvider<JObject> Resolve(Entity source, EntityLiquidAdapter destination, CustomFieldFactoryProvider<JObject> destMember,
            ResolutionContext context)
        {
            return _customFieldFactoryProvider;
        }
    }
}