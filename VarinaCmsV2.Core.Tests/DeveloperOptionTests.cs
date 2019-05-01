using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.Core.FrontEndOptions;
using System.Linq;

namespace VarinaCmsV2.Core.Tests
{
    [TestClass]
    public class DeveloperOptionTests
    {
        [TestMethod]
        public void ShouldReadDataFromJsonFile()
        {
            var cfgPath = "./../../front-developer-options.json";
           
            FrontEndDeveloperOptions.Instance.Images.ResizeOptions.ForEach(x=>Console.WriteLine(x.Height));
            FrontEndDeveloperOptions.Instance.Images.AllowedTypes.ForEach(Console.WriteLine);
            Assert.AreEqual(FrontEndDeveloperOptions.Instance.EntityRelatedItemsLoadOptions.First().Order, Common.OrderOption.Random);
        }
    }
}
