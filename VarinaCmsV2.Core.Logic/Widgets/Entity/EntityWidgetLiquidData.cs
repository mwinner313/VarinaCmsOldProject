using System;
using System.Collections.Generic;
using VarinaCmsV2.Core.Contracts.Widget;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Logic.Widgets.Entity
{
    public class EntityWidgetLiquidData:BaseWidgetLiquidData
    {
        public List<EntityLiquidAdapter> Entities { get; set; }
    }
}
