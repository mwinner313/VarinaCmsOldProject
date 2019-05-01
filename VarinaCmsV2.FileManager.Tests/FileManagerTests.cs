using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VarinaCmsV2.FileManager.Tests
{
    [TestClass]
    public class FileManagerTests
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            string fullPath = "./1/2/3/4/5/test/test.txt";
            int count = 1;

            string fileNameOnly = Path.GetFileNameWithoutExtension(fullPath);
            string extension = Path.GetExtension(fullPath);
            string dir = Path.GetDirectoryName(fullPath);
            string newFullPath = fullPath;

           

            while (File.Exists(newFullPath))
            {
                string tempFileName = $"{fileNameOnly}({count++})";
                newFullPath = Path.Combine(dir, tempFileName + extension);
            }
            //don't need to check if it exists first
            Directory.CreateDirectory(dir);
            var fileInfo = new FileInfo(newFullPath);

            var task =  Task.Run(() => fileInfo.Create());
            await task;
        }
    }
}
