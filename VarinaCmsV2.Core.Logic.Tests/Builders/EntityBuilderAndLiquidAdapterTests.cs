using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotLiquid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Configuration.Base;
using VarinaCmsV2.Core.Logic.Builders;
using VarinaCmsV2.Core.Logic.CustomFields.Factories;
using VarinaCmsV2.Core.Logic.Providers;
using VarinaCmsV2.Core.Logic.Tests.Mocks;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.IocCofig;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Logic.Tests.Builders
{
    [TestClass]
    public class EntityBuilderAndLiquidAdapterTests
    {
        private readonly CustomFieldFactoryProvider<JObject> _factoryProvider =
            AppObjectFactory.GetContainer().GetInstance<CustomFieldFactoryProvider<JObject>>();
        [TestMethod]
        public  Task ShouldValidateIncommingFields()
        {
            // arrange
            var builder = new GenericIEntityBuilder<Entity,Category>(_factoryProvider,new UrlBuilder());
            var entity = new Entity() {Handle = "test url ssss     ssss"};
            var textFieldDefenition = new FieldDefenition() { Title = "text1", Type = "text", IsRequired = true ,Id = Guid.NewGuid()};
            var numberFieldDefenition = new FieldDefenition() { Title = "number", Type = "number", IsRequired = true, Id = Guid.NewGuid() };

            var entityScheme = new EntityScheme()
            {
                FieldDefenitions = new List<FieldDefenition>()
                {
                    textFieldDefenition,
                    numberFieldDefenition
                }
            };

          
            var textFactory = new TextFieldFactory();
            var numberFactory = new NumberFieldFactory();


            // act 
            builder.SetBuildingEntity(entity,entityScheme,new Category() {FieldDefenitions = new List<FieldDefenition>()});

            var text = textFactory.CreateNew("text1", "hello world",null);
            var number = numberFactory.CreateNew("number", 5, null);

            var textField = new Field() { RawValue = text.Value, FieldDefenition = textFieldDefenition };
            var numberField=new Field() {RawValue = number.Value,FieldDefenition = numberFieldDefenition};

           

            var liquidAdapter = new EntityLiquidAdapter();
            liquidAdapter.FieldFactoryProvider = _factoryProvider;
            liquidAdapter.Entity = builder.GetResult();
            Template t =Template.Parse("text is {{entity.text1}} and number is {{entity.number}}");
            var actual = t.Render(Hash.FromAnonymousObject(new {entity = liquidAdapter}));
            //assert
            Console.WriteLine(actual);

            return Task.FromResult(0);
        }
    }
}
