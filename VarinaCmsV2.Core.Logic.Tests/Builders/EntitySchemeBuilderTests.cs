//using System;
//using System.Collections.Generic;
//using System.Runtime.Remoting.Messaging;
//using System.Threading.Tasks;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Newtonsoft.Json.Linq;
//using VarinaCmsV2.Common;
//using VarinaCmsV2.Core.Contracts.Providers;
//
//using VarinaCmsV2.Core.Logic.Builders;
//using VarinaCmsV2.Core.Logic.CustomFields.Factories;
//using VarinaCmsV2.Core.Logic.Providers;
//using VarinaCmsV2.Core.Logic.Tests.Mocks;
//using VarinaCmsV2.Data.DbContexts;
//using VarinaCmsV2.DomainClasses.Entities;
//using VarinaCmsV2.IocCofig;

//namespace VarinaCmsV2.Core.Logic.Tests.Builders
//{
//    [TestClass]
//    public class EntitySchemeBuilderTests
//    {
//        private readonly EntitySchemeBuilder _SchemeBuilder = new EntitySchemeBuilder(AppObjectFactory.GetContainer().GetInstance<CustomFieldFactoryProvider<JObject>>(), new LangServ(), new UrlBuilder(),new AppDbContext());
//        private readonly EntityScheme _scheme = new EntityScheme() { Title = "test", Url = "test entity" };
//        private readonly FieldDefenition _fd1 = new FieldDefenition()
//        {
//            IsRequired = true,
//            IsArray = true,
//            Title = "authorName",
//            LocalizedNames = new List<LocalizedName>()
//                {
//                    new LocalizedName() { LanguageName = "en-US" , Name = "authorName"},
//                    new LocalizedName() {LanguageName = "fa-IR",Name = "نویسنده"}
//                },
//            Description = "some descrtion",
//            Type = "text",
//            DefaultValue = new TextFieldFactory().CreateNew("authorName", "hello").Value
//        };

//        [TestMethod]
//        public async Task EntitySchemeCreationTest()
//        {
//            _SchemeBuilder.SetBuildingScheme(_scheme);

          
//            await _SchemeBuilder.AddFieldDefenition(_fd1);
//            var res = _SchemeBuilder.GetResult();
//            Assert.AreEqual(res.FieldDefenitions.Count, 1);

//            var entity =new Entity() { Handle = "test sss"};
//            var entityBuilder = new EntityBuilder(AppObjectFactory.GetContainer().GetInstance<CustomFieldFactoryProvider<JObject>>(), new LangServ())
//            {
//                UrlBuilder = new UrlBuilder()
//            };
//            entityBuilder.SetBuildingEntity(entity);
//            entityBuilder.SetScheme(_scheme);
//             entityBuilder.AddField(new Field()
//            {
//                FieldDefenition = _fd1,
//                JobjectValue = new JObject()
//            });
//            var resEntity = entityBuilder.GetResult();
//        }

//    }
//}
