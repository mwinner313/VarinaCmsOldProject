using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.Core.Contracts.Configuration.Base;

namespace VarinaCmsV2.Core.Logic.Providers
{
    public class JsonCustomFieldFactoryProvider : CustomFieldFactoryProvider<JObject>
    {
        public JsonCustomFieldFactoryProvider(List<CustomFieldFactory<JObject>> factories)
        {
            Factories = factories;
        }
        private List<CustomFieldFactory<JObject>> Factories { get;  }
        public override List<CustomFieldFactory<JObject>> GetAvailableFactories()
        {
            return Factories;
        }

        public override CustomFieldFactory<JObject> GetFieldFactory(string fieldType)
        {
            return Factories.FirstOrDefault(x => x.FieldType == fieldType);
        }
    }
}
