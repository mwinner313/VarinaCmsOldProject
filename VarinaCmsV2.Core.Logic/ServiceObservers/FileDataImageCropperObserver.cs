using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Core.FrontEndOptions;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.ServiceObservers;
using VarinaCmsV2.Core.Services.File;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.FileManager.Files;
using VarinaCmsV2.ViewModel.FileData;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.Core.Logic.ServiceObservers
{
    public class FileDataImageCropperObserver : FileDataObserver
    {
        public const string MetaName = "resized-image";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        public FileDataImageCropperObserver(IUnitOfWork unitOfWork, IFileManager fileManager)
        {
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
        }

        public override void FileAdded(FileResponse res, FileAddRequest req)
        {
            if (!IsImage(res.Model.Path)) return;

            FrontEndDeveloperOptions.Instance.Images.ResizeOptions.ForEach(opt =>
            {
                var resizedPath = CreateFileName(res.Model, opt);
                CreateImageOnServer(res, opt, resizedPath);

                var meta = new Meta
                {
                    MetaName = MetaName,
                    TargetId = res.Model.Id,
                    TargetType = FileData.MetaTypeName,
                    MetaData =
                        JObject.FromObject(new ImageData
                        {
                            Path = "/" + resizedPath.Replace(AppDir, "").Replace("\\", "/"),
                            SizeName = opt.Name,
                            Width = opt.Width,
                            Height = opt.Height,
                            Name = Path.GetFileNameWithoutExtension(resizedPath)
                        })
                };

                _unitOfWork.Add(meta);
                res.Model.MetaData.Add(meta.MapToViewModel());
            });

          
        }

       

        private void CreateImageOnServer(FileResponse res, ResizeOption opt, string resizedPath)
        {
            var image = new Bitmap(AppDir + res.Model.Path);
            var newSize = new Size(opt.Width, opt.Height);
            var resized = new Bitmap(image, newSize);
            resized.Save(resizedPath, ImageFormat.Jpeg);

            image.Dispose();
            resized.Dispose();
        }
        public override void FileEdited(FileResponse res, FileEditRequest req)
        {
            //foreach (var resizedImage in GetResizedImageMetaData(req.FileId).Select(x=>x.MetaData.ToObject<ResizedImage>()))
            //{

            //}to do
        }

        public override void FileDeleted(FileResponse res, FileDeleteRequest req)
        {
            GetResizedImageMetaData(req.FileId)
                .ForEach(x =>
                {
                    _fileManager.Delete(AppDir + x.MetaData.ToObject<ImageData>().Path);
                    _unitOfWork.Delete(x);
                });
        }


        private string CreateFileName(FileDataViewModelWithMeta fileData, ResizeOption opt)
        {
            var commpressedPath = Path.GetDirectoryName(AppDir + fileData.Path) + "\\" + fileData.Name +
                                  $"-min-{opt.Width}X{opt.Height}";
            var tempPath = commpressedPath;
            var counter = 0;
            while (File.Exists(tempPath + ".jpeg"))
            {
                tempPath = commpressedPath + $"({counter})";
                counter++;
            }
            return tempPath + ".jpeg";
        }
        private bool IsImage(string path)
        {
            return FrontEndDeveloperOptions.Instance.Images.AllowedTypes.Contains(Path.GetExtension(AppDir + path));
        }
        private List<Meta> GetResizedImageMetaData(Guid fileId)
        {
            return _unitOfWork.Set<Meta>()
                .Where(x => x.MetaName == MetaName &&
                            x.TargetType == FileData.MetaTypeName &&
                            x.TargetId == fileId)
                .ToList();
        }


    }
}