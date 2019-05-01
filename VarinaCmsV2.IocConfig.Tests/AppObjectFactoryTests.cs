using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.Common.MvcFilters;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.ServiceObservers;
using VarinaCmsV2.Core.Services.File;
using VarinaCmsV2.Core.Web.ActionFilters;
using VarinaCmsV2.IocCofig;

namespace VarinaCmsV2.IocConfig.Tests
{
    /// <summary>
    /// Summary description for AppObjectFactoryTests
    /// </summary>
    [TestClass]
    public class AppObjectFactoryTests
    {


        [TestMethod]
        public void ShouldFindExcutingCodeAssemblyRegistries()
        {
            AppObjectFactory.FindRegistries().ForEach(Console.WriteLine);
        }
        [TestMethod]
        public void ShouldCreateConcreteClassesWithDefaultConvetion()
        {
            var c = AppObjectFactory.GetContainer();
            var someObj = c.GetInstance<TelegramErrorLoggerFilterAttribute>();
            Assert.IsNotNull(someObj);
        }
        [TestMethod]
        public void ShouldFillDependenciesOfAConstructedObj()
        {
            var c = AppObjectFactory.GetContainer();
            var someObj = new MvcEnableEntityValidationAttribute();
            c.BuildUp(someObj);
            Assert.IsNotNull(someObj.EntitySchemesDataService);
        }
        [TestMethod]
        public void ShouldGetInfrustructureLogics()
        {
            var c = AppObjectFactory.GetContainer();
            var list = c.GetInstance<List<IBaseBeforeAddingEntityLogic>>();
            foreach (var item in list)
            {
                Console.WriteLine(item);
                Assert.IsNotNull(item.Container);
            }
        }

        [TestMethod]
        public void ShouldWireUpDefaultConcereteTypes()
        {
            var c = AppObjectFactory.GetContainer();
            Assert.IsTrue(c.GetInstance<IFileService>() is FileServiceSubject);
        }

    }
}
