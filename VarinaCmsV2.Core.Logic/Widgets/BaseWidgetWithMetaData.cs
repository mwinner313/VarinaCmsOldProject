using System;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using StructureMap;
using VarinaCmsV2.Core.Contracts.Widget;

namespace VarinaCmsV2.Core.Logic.Widgets
{
    public abstract class BaseWidgetWithMetaData<TMetaData> :BaseWidget where TMetaData : class, new()
    {
        protected TMetaData MetaData { get; set; }
        public void DeserializeMetaData(JObject metaData)
        {
            MetaData = metaData.ToObject<TMetaData>();
        }

    }
}