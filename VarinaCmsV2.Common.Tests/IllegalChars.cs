using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VarinaCmsV2.Common.Tests
{
    [TestClass]
    public class IllegalChars
    {
        [TestMethod]
        public void ShowldRemoveIllegalChars()
        {
            const string Illegals = "\"M\"\\a/ry/ h**ad:>> a\\/:*?\"| li*tt|le|| la\"mb.?";
            var str = "\"hello";
            //Illegals.Split(new [] {""}, StringSplitOptions.None).ToList().ForEach(x =>
            //{
            //    str = str.Replace(, "");
            //});
            Console.WriteLine(str.Replace("\"", ""));
        }
    }
}
