using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VarinaCmsV2.Common.Tests
{
    [TestClass]
    public class RegexPatternsTests
    {
        [TestMethod]
        public void ShouldMatchDate()
        {
          var res=Regex.IsMatch("1396/12/31",RegexPattern.Date);
            Assert.AreEqual(res,true);
        }
        [TestMethod]
        public void ShouldNotMatchDate()
        {
            var res = Regex.IsMatch("111111/22/33", RegexPattern.Date);
            Assert.AreEqual(res, false);
        }

        [TestMethod]
        public void ShouldMatchDateTime()
        {
            var res = Regex.IsMatch("1396/12/21-19:59", RegexPattern.DateTime);
            Assert.AreEqual(res, true);
        }
        [TestMethod]
        public void ShouldNotMatchDateTime()
        {
            var res = Regex.IsMatch("111111/22/33", RegexPattern.DateTime);
            Assert.AreEqual(res, false);
        }
    }
}
