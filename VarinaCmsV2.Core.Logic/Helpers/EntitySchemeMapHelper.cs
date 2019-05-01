using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.Core.Contracts.WebClientServices.Entities;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.Page;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class EntitySchemeMapHelper
    {
        public static EntitySchemeLiquidViewModel AsLiquidAdapted(this EntityScheme scheme)
        {
            return Mapper.Map<EntitySchemeLiquidViewModel>(scheme);
        }

        public static EntitySchemeLiquidViewModel AsLiquidAdaptedWithEntities(this EntityScheme scheme, PaginatedEntities entities)
        {
            var adapted = scheme.AsLiquidAdapted();
            adapted.Entities = entities.Entities.AsLiquidAdapted();
            adapted.AllPagesCount = entities.AllPagesCount;
            adapted.CurrentPage = entities.CurrentPage;
            return adapted;
        }
    }
}
