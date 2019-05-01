using System;
using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.ViewModel.Entities
{
    public class EntitySchemeLiquidViewModel:PaginatedUrlableLiquidAdapter
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public Guid Id { get; set; }
        public List<EntityLiquidAdapter> Entities { get; set; }
        public List<FieldDefenitionLiquidVeiwModel> FieldDefenitions { get; set; }
        public List<FieldDefenitionGroupLiquidVeiwModel> FieldDefenitionGroups { get; set; }
    }
}