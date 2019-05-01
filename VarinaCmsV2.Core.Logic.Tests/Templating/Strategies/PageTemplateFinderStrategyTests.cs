using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.Core.Logic.Templating.Strategies;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Logic.Tests.Templating.Strategies
{
    [TestClass]
    public class PageTemplateFinderStrategyTests
    {
        private readonly ILiquidTemplateFinderStrategy _strategy = new PageTemplateFinderStrategy();
        [TestMethod]
        public void ShouldGetCorrectTemplate()
        {
            var page = new Page()
            {
                Handle = "about-us"
            };

            _strategy.GetTemplatePathDefault(page);

        }

    }
}
