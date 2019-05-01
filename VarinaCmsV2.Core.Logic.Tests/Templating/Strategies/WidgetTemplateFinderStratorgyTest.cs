using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.Core.Logic.Templating.Strategies;
using VarinaCmsV2.DomainClasses.Entities.Widgets;

namespace VarinaCmsV2.Core.Logic.Tests.Templating.Strategies
{
    [TestClass]
    public class WidgetTemplateFinderStratorgyTest
    {
        private readonly ILiquidTemplateFinderStrategy _strategy = new WidgetTemplateFinderStrategy();
        [TestMethod]
        public void ShouldFindCorrectTemplate()
        {
            var widget = new Widget()
            {
                Handle = "test"
            };
            if (File.Exists("../../widget-test.html"))
            {
                File.Delete("../../widget-test.html");
            }
            _strategy.GetTemplatePathDefault(widget);


            File.Create("../../widget-test.html");
            _strategy.GetTemplatePathDefault(widget);
        }
    }
}
