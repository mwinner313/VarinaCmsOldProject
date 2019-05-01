using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using VarinaCmsV2.Core.Logic.Decorators;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Logic.Tests.Decorators
{
    [TestClass]
    public class CurrentUserLiquidDataDecoratorsTests
    {
        [TestMethod]
        public void ShoulAddCurrentUserToWrappe()
        {

            var str =
                Template.Parse("{{data.currentUser.name}}")
                    .Render();
            Console.WriteLine(str);
        }
    }
}
