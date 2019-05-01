using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DotLiquid;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Widget;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.Common;
using System;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Logic.Widgets.Entity
{
    internal class EntityWidget : BaseWidgetWithMetaData<EntityWidgetMetaData>
    {
        private readonly IEntityDataService _entityDataService;

        public EntityWidget()
        {
            _entityDataService = Container.GetInstance<IEntityDataService>();
        }

        public override string Type { get; } = "entity";

        public override BaseWidgetLiquidData Parse(JObject widgetMetaData)
        {
            DeserializeMetaData(widgetMetaData);
            var entities =
                _entityDataService.Query
                    .Include(x => x.Tags)
                    .Include(x => x.Fields)
                    .Include(x => x.Fields.Select(f => f.FieldDefenition))
                    .Include(x => x.Creator)
                    .Include(x => x.RelatedCategories)
                    .Include(x => x.PrimaryCategory)
                    .Where(x => x.SchemeId == MetaData.SchemeId).GetWithAppliedOrder(MetaData.OrderOption).GetWithApliedTimeRange(MetaData.TimeRange);

            return new EntityWidgetLiquidData() { Entities = entities.Take(MetaData.Count).ToList().AsLiquidAdapted() };
        }

    }
}