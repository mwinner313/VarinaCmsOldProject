using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VarinaCmsV2.Data.Tests.SetupDataBaseInitializer
{
    [TestClass]
    public class SetupDataBaseInitializerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var context = new TestContext();
            context.Tests.Add(new Test() { Text = "he" });
            context.SaveChanges();
            Assert.AreEqual(context.Tests.Count(), 1);
        }
    }
}
