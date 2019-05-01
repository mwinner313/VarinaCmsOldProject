//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using VarinaCmsV2.Core.Contracts.Templating;
//using VarinaCmsV2.Core.Logic.Templating;
//using VarinaCmsV2.Core.Logic.Templating.Strategies;
//using VarinaCmsV2.Data.DbContexts;
//using VarinaCmsV2.DomainClasses.Entities;
//using VarinaCmsV2.DomainClasses.Entities.Widgets;
//using VarinaCmsV2.IocCofig;

//namespace VarinaCmsV2.Core.Logic.Tests.Templating
//{
//    [TestClass]
//    public class TemplateFinderFactoryTests
//    {
//        private readonly ITemplateFinderFactory _factory = new TemplateFinderFactory("../..", AppObjectFactory.GetContainer());

//        [TestMethod]
//        public void ShouldGetCorrectStrategy()
//        {
//            // arrange
//            var widget = new Widget() { Handle = "whether" };
//            var page = new Page() { Handle = "about-us" };
//            var tag = new Tag() { Handle = "game", EntityScheme = new EntityScheme() { Handle = "articles" } };
//            var entity = new Entity() { Scheme = new EntityScheme() { Handle = "product" } };

//            //act
//            var tagstrategy = _factory.GetStrategy(tag);
//            var entitystrategy = _factory.GetStrategy("../..",entity);
//            var pagestrategty = _factory.GetStrategy("../..",page);
//            var widgetstrategy = _factory.GetStrategy("../..",widget);
//            var homePageStrategy = _factory.GetStrategy("../..",null);
//            //assert
//            Assert.IsInstanceOfType(tagstrategy,typeof(EntityTagTemplateFinderStrategy));
//            Assert.IsInstanceOfType(entitystrategy, typeof(EntityTemplateFinderStrategy));
//            Assert.IsInstanceOfType(pagestrategty, typeof(PageTemplateFinderStrategy));
//            Assert.IsInstanceOfType(widgetstrategy, typeof(WidgetTemplateFinderStrategy));
//            Assert.IsInstanceOfType(homePageStrategy, typeof(HomePageTemplateFinderStrategy));
//        }
//    }
//}
