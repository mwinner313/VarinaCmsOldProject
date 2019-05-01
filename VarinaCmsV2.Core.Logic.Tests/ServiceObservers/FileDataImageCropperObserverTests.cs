using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.Core.Logic.ServiceObservers;
using VarinaCmsV2.Core.Services.File;
using VarinaCmsV2.Services.Tests;
using VarinaCmsV2.ViewModel.FileData;

namespace VarinaCmsV2.Core.Logic.Tests.ServiceObservers
{
    [TestClass]
    public class FileDataImageCropperObserverTests
    {


        [TestMethod]
        public void ShouldCreateFilesWithMetas()
        {

            CurrentIdentityMocker.Get();

            var ctx = new MockUnitOfWork();
            var cropper = new FileDataImageCropperObserver(ctx,new FileManager.Files.FileManager());
             cropper.FileAdded(new FileResponse()
            {
                Model = new FileDataViewModelWithMeta()
                {
                    CreateDateTime = DateTime.Now,
                    CreatorName = "ali",
                    Extension = ".jpg",
                    Path = "./../../pic.jpg",
                    Name = "pic",
                    Size = new FileInfo("./../../pic.jpg").Length,
                    UpdateDateTime = DateTime.Now
                }
            }, new FileAddRequest()
            {
                
            });

        }
    }
}
