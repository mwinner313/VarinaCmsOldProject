using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.Core.Logic.CustomFields.Factories;


namespace VarinaCmsV2.Services.Tests
{
    [TestClass]
    public class CustomFieldsLiquidIntegrateTests
    {
        [TestMethod]
        public void ShouldRednerTextFieldWithLiquidTest()
        {
           //arange
           var factory=new TextFieldFactory();
            var field = factory.CreateNew("greetings", "hello",null);
            Template template = Template.Parse("{{ greetings }}");
            Assert.AreEqual("hello", template.Render(Hash.FromDictionary(new ConcurrentDictionary<string, object>
            {
                [field.FieldName]=field.ToLiquid()
            })));
        }
    }
}
