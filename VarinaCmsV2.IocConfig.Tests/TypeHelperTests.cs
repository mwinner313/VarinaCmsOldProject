using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using VarinaCmsV2.IocCofig;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.IocConfig.Tests
{
    [TestClass]
    public class TypeHelperTests
    {
        [TestMethod]
        public void ShouldSearchAllRefrencedAsseblies()
        {
            var list = TypeHelper.FindListOf<IRoleList>();
            var list2 = TypeHelper.FindListOf<IPremissionList>();

            var exampleOfInjected = AppObjectFactory.GetContainer().GetInstance<IIdentityManager>();
        }
    }
}
