using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using VarinaCmsV2.AutoMapperProfiles;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;
using VarinaCmsV2.Core.Logic.Facades;
using VarinaCmsV2.Core.LogicFacade;
using VarinaCmsV2.IocCofig;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Logic.Tests.LogicFacades
{
    [TestClass]
    public class EntitySchemeFacadeTests
    {
        public EntitySchemeFacadeTests()
        {
            AutomapperConfiguration.Cofigure(AppObjectFactory.GetContainer());
        }
        [TestMethod]
        public async Task ShouldCreateNewSchemeTest()
        {
            var schemeCreator = AppObjectFactory.GetContainer().GetInstance<IEntitySchemeFacade>();
            var serializeSetting = new JsonSerializer()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            
        await    schemeCreator.AddNewSchemeToSystemAsync(new EntitySchemeViewModel()
            {
                Handle = "hey i have spaces ",
                Description = "some dec",
                Title = "good title",
                Url = "good url",
                FieldDefenitions = new List<FieldDefenitionViewModel>()
                {
                    new FieldDefenitionViewModel()
                    {
                        Handle = "name",
                        Title = "نام",
                        IsRequired = true,
                        Type = "text",
                        DefaultValue =  JObject.FromObject(new TextTemplate() {Text = "myname"},serializeSetting),
                        Order = 1,
                  
                    }
                }
            });
        }
    }
}
