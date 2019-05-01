using System;
using System.Web;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Helpers;

namespace VarinaCmsV2.Web.Tests
{
    [TestClass]
    public class AntiForgeryTokentests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Console.Write(AntiForgery.GetHtml());
        }
    }
}
