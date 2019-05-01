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
    public class NumberFieldFactoryTests
    {
        [TestMethod]
        public void ShouldValidateEmptyString()
        {
            var emptyNumber = new NumberTemplate() { Number = "" };
            var factory = new NumberFieldFactory();
            var actual = factory.IsValidForField(JObject.FromObject(emptyNumber));
            Assert.IsTrue(actual);

            var filledNumber= new NumberTemplate() { Number = "1234566789021354" };
            var actual2 = factory.IsValidForField(JObject.FromObject(filledNumber));
            Assert.IsTrue(actual2);

            var worngNumber = new NumberTemplate() { Number = "123456678adsassd9021354" };
            var actual3 = factory.IsValidForField(JObject.FromObject(worngNumber));
            Assert.IsFalse(actual3);
        }

        [TestMethod]
        public void ShouldValidateStringData()
        {
            var factory = new NumberFieldFactory();
            Assert.IsTrue(factory.IsValidForField("12345"));
        }
    }
}
