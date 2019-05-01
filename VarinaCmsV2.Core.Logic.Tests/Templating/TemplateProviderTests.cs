using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.Core.Logic.Templating;
using VarinaCmsV2.Data.DbContexts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.IocCofig;

namespace VarinaCmsV2.Core.Logic.Tests.Templating
{
    [TestClass]
    public class TemplateProviderTests
    {
        ITemplateProvider _templateProvider = new CacheTemplateProvider(new TemplateFinderFactory("../..",AppObjectFactory.GetContainer())) {TemplatesBasePath = "../.."};
        [TestMethod]
        public void ShuoldGetCorrectFileTemplateAndCache()
        {
            var category = new Category()
            {
                Handle = "game",
                Scheme = new EntityScheme()
                {
                    Handle = "article"
                }
            };
            var templateString = _templateProvider.GetTemplateString(category);
            Assert.AreEqual(templateString,$@"<title>hello new game category</title>");

            var templateString1 = _templateProvider.GetTemplateString(category);
            Assert.AreEqual(templateString1, $@"<title>hello new game category</title>");

            using (var r = File.AppendText("../../category-article-game.html"))
            {
                r.Write("test");
            }   
               

            var templateString2 = _templateProvider.GetTemplateString(category);
        }
    }
}
