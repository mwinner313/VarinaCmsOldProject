using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.Core.Logic.Templating.Strategies;

namespace VarinaCmsV2.Core.Logic.Tests.Templating.Strategies
{
    [TestClass]
    public class HomePageTemplateFinderStrategyTests
    {
        [TestMethod]
        public void ShouldFindCorrectHomePage()
        {
            var strategy=new HomePageTemplateFinderStrategy();
          Console.WriteLine(strategy.GetTemplatePathDefault());
        }
    }
}
