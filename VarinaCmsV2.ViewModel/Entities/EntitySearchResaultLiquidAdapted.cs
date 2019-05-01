using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.ViewModel.Entities
{
    public class EntitySearchResaultLiquidAdapted : UrlableLiquidAdapter
    {
        public EntitySearchResaultLiquidAdapted()
        {
        }

        public string SearchText { get; set; }
        public List<EntityLiquidAdapter> Entities { get; set; }
        public int Count { get; set; }
        public EntitySchemeLiquidViewModel Scheme { get; set; }
    }
}