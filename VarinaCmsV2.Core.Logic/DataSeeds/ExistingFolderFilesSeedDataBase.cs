using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.FileManager;
using VarinaCmsV2.FileManager.Directories;

namespace VarinaCmsV2.Core.Logic.DataSeeds
{
    public class ExistingFolderFilesSeedDataBase
    {
        public void Seed(IUnitOfWork unitOfWork,string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + path);
            }


            var files = dir.GetFiles().ToList();
            var fileSet = unitOfWork.Set<FileData>();
            files.ForEach(x =>
            {
                var filePath ="/"+ x.FullName.Replace(AppDomain.CurrentDomain.BaseDirectory, "").Replace("\\","/");
                
                    fileSet.AddOrUpdate(f=>f.Path,new FileData()
                    {
                        Path = filePath,
                        Name = x.Name.Replace(x.Extension,""),
                        AccessType = AccessType.Public,
                        CreateDateTime = DateTime.Now,
                        UpdateDateTime = DateTime.Now,
                        Size = x.Length,
                        Extension = x.Extension
                    });
                
            });
            unitOfWork.SaveChanges();
            foreach (DirectoryInfo subdir in dirs)
            {
                 Seed(unitOfWork, subdir.FullName);
            }
           
        }

    }
}
