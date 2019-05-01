using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VarinaCmsV2.Common.Tests
{
    [TestClass]
    public class UrlBuilderTests
    {
        [TestMethod]
        public void ShouldCreateCorrectUrlSegment()
        {
            var urlBuilder = new UrlBuilder();
            Console.WriteLine(urlBuilder.GenrateUrlSegment("test   test   m.ggg@@$%^^&&&%$#@ hihihi"));
            Console.Write(urlBuilder.GenrateUrlSegment("test --------- test   m.ggg@@$%^^&&&%$#@ hihihi"));
            Console.Write(urlBuilder.GenrateUrlSegment("آآآآآموزش   آ ااااچجحخهعغفقثصض گکمنتالبیسش/.وئدذرزطظ"));
            var raw = "چجحخهعغفقآثصضشسیبلاتنمکگوئدذرزطظ";
               Assert.AreEqual(urlBuilder.GenrateUrlSegment(raw).Length,raw.Length);
        }
        [TestMethod]
        public void someStringTest()
        {
            var url = "http://mywebsite.com";
            Console.Write(url.Insert(url.IndexOf("://", StringComparison.Ordinal)+3, "www."));
        }
    }
}
