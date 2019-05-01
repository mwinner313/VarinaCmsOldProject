using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Core.Logic.CustomFields.Factories;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;

namespace VarinaCmsV2.Core.Logic.Tests.CustomFieldTests
{
    [TestClass]
    public class FileFieldFactoryTests
    {
        [TestMethod]
        public void ShowGetMimeType()
        {
            Console.WriteLine(new FileInfo("./hhh/test.d.da.ssdds.ssa.exe").Extension);
        }
        [TestMethod]
        public void ShouldValidateFileExteinsion()
        {
            var factory = new FileFieldFactory();
            var isPdfValid = factory.IsValidForField(JObject.FromObject(new FileFieldTemplate
            {
                Name = "test",
                Extention = ".pdf",
                Path = "./dsadsadasd/test.pdf"
            }));
            Assert.IsTrue(isPdfValid);
            var isExeValid = factory.IsValidForField(JObject.FromObject(new FileFieldTemplate
            {
                Name = "test",
                Extention = ".exe",
                Path = "./dsadsadasd/test.exe",
            }));
            Assert.IsFalse(isExeValid);
        }

    }
}
