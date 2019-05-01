using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Core.Logic.CustomFields.Factories;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;

namespace VarinaCmsV2.Core.Logic.Tests.CustomFieldTests
{
    [TestClass]
    public class HtmlFieldFactoryTests
    {
        [TestMethod]
        public void IsValidForFieldTest()
        {
            var f = new HtmlFieldFactory() {};
         var actual=   f.IsValidForField(JObject.FromObject(new TextTemplate() {Text = "ss"}));
            Assert.IsFalse(actual);

            var actual2 = f.IsValidForField(JObject.FromObject(new HtmlFieldTemplate() { Content = "ss" }));
            Assert.IsTrue(actual2);
        }
    }
}
