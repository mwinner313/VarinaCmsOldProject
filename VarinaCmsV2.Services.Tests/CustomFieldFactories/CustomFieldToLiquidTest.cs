using System;
using System.Collections.Concurrent;
using DotLiquid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Configuration.Base;
using VarinaCmsV2.Core.Logic.CustomFields.Factories;
using VarinaCmsV2.Core.Logic.Providers;
using VarinaCmsV2.IocCofig;

namespace VarinaCmsV2.Services.Tests.CustomFieldFactories
{
    [TestClass]
    public class CustomFieldToLiquidTest
    {
        private readonly CustomFieldFactoryProvider<JObject> _factroyProvider;

        public CustomFieldToLiquidTest()
        {
            _factroyProvider = AppObjectFactory.GetContainer().GetInstance<CustomFieldFactoryProvider<JObject>>();
        }

        [TestMethod]
        public void TextToLiquidTest()
        {
            //arrange
            var factory = new TextFieldFactory();
            var field = factory.CreateNew("test", "hello",null);

            Template template = Template.Parse("{{ test }}");
            Console.WriteLine(factory.GetType().AssemblyQualifiedName);
            Assert.AreEqual("hello", template.Render(Hash.FromDictionary(new ConcurrentDictionary<string, object>
            {
                [field.FieldName] = field.ToLiquid()
            })));
        }
    }
}
