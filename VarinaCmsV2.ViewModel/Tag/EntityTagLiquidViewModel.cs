using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.ViewModel.Tag
{
    public class EntityTagLiquidViewModel: PaginatedUrlableLiquidAdapter
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public EntitySchemeLiquidViewModel EntityScheme { get; set; }
        public List<EntityLiquidAdapter> Entities { get; set; }
    }
}