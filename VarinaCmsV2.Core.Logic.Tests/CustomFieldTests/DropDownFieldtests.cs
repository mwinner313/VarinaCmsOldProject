using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Logic.CustomFields.Factories;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;
using VarinaCmsV2.Core.Logic.CustomFields.MetaDataTemplate;
using VarinaCmsV2.IocCofig;

namespace VarinaCmsV2.Core.Logic.Tests.CustomFieldTests
{
    [TestClass]
    public class DropDownFieldtests
    {
        [TestMethod]
        public void SholuldValidateAvalibleSelectedDropOption()
        {
            var factory = AppObjectFactory.GetContainer().GetInstance<CustomFieldFactoryProvider<JObject>>()
                .GetFieldFactory("dropdown");

            Assert.IsInstanceOfType(factory, typeof(DropDownListFieldFactory));

            var selectedOption = new DropDownListFieldTemplate() { Name = "طبع گرم", Value = "red" };
            var notValidSelectedOption = new DropDownListFieldTemplate() { Value = "blue", Name = "طبع گرم و سرد" };
            var dropDownFieldMetaData = new DropDownFieldMetaData()
            {
                AvalibleOptions = new List<DropDownListFieldTemplate>()
                {
                    selectedOption,
                    new DropDownListFieldTemplate() {Name = "طبع سرد", Value = "green"}
                }
            };

            Assert.IsTrue(factory.IsValidForField(JObject.FromObject(selectedOption), JObject.FromObject(dropDownFieldMetaData)));
            Assert.IsTrue(factory.IsValidForField(selectedOption.Value, JObject.FromObject(dropDownFieldMetaData)));

            Assert.IsFalse(factory.IsValidForField(JObject.FromObject(notValidSelectedOption), JObject.FromObject(dropDownFieldMetaData)));
            Assert.IsFalse(factory.IsValidForField(notValidSelectedOption.Value, JObject.FromObject(dropDownFieldMetaData)));
        }
    }
}
