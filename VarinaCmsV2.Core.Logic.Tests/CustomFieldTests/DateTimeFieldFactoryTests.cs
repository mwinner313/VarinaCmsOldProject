using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Core.Logic.CustomFields.Factories;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;

namespace VarinaCmsV2.Core.Logic.Tests.CustomFieldTests
{[TestClass]
    public class DateTimeFieldFactoryTests
    {
        [TestMethod]
        public void ShouldValidateEmptyString()
        {
            var dateTimeTemplate=new DateTimeAndDateTemplate() {DateTimeString = ""};
            var factory=new DateTimeFieldFactory();
            var actual=  factory.IsValidForField(JObject.FromObject(dateTimeTemplate));

            var numberField=new NumberTemplate() {Number = ""};
            var actual2= factory.IsValidForField(JObject.FromObject(numberField));
            Assert.IsTrue(actual);
            Assert.IsFalse(actual2);

           
        }
    }
}
