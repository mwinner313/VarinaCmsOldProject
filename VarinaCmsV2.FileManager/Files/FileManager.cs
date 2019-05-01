using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using VarinaCmsV2.Common.WebApi;

namespace VarinaCmsV2.FileManager.Files
{
    public class FileManager : IFileManager
    {
        public async Task<FileSaveResult> SaveAsync(HttpContent file, string directoryPath)
        {
            Directory.CreateDirectory(directoryPath);
            var data = await file.ReadAsMultipartAsync(new CustomMultipartFormDataStreamProvider(directoryPath));
            if (IsImage(data.FileData.First().LocalFileName)) MinimizeImageSize(data.FileData.First().LocalFileName);
            var fileInfo = new FileInfo(data.FileData.First().LocalFileName);
            return new FileSaveResult()
            {
                Size = fileInfo.Length,
                SavedFilePath = "/" + fileInfo.FullName.Replace(AppDomain.CurrentDomain.BaseDirectory, "").Replace("\\", "/"),
                SavedFileName = fileInfo.Name.Replace(fileInfo.Extension, ""),
                Extension = fileInfo.Extension
            };
        }

        private bool IsImage(string path)
        {
            var imageTypes = new string[] { ".png", ".jpeg", ".jpg" };
            return imageTypes.Contains(Path.GetExtension(path));
        }

        private void MinimizeImageSize(string path)
        {
            var image = new Bitmap(new MemoryStream(File.ReadAllBytes(path)));
            var newSize = new Size(image.Width, image.Height);
            var resized = new Bitmap(image, newSize);

            File.Delete(path);

            resized.Save(path, ImageFormat.Jpeg);

            image.Dispose();
            resized.Dispose();
        }

        public FileSaveResult Save(HttpPostedFileBase file, string fullPath = null)
        {
            fullPath = fullPath ?? AppDomain.CurrentDomain.BaseDirectory + WebConfigurationManager.AppSettings.Get("site-upload-path") + file.FileName;

            int count = 1;

            string fileNameOnly = Path.GetFileNameWithoutExtension(fullPath);
            string extension = Path.GetExtension(fullPath);
            string dir = Path.GetDirectoryName(fullPath);
            string newFullPath = fullPath;
            string tempFileName = string.Empty;
            while (File.Exists(newFullPath))
            {
                tempFileName = $"{fileNameOnly}({count++})";
                newFullPath = Path.Combine(dir, tempFileName + extension);
            }

            Directory.CreateDirectory(dir);
            file.SaveAs(newFullPath);
            return new FileSaveResult()
            {
                SavedFilePath = "/" + newFullPath.Replace(AppDomain.CurrentDomain.BaseDirectory, "").Replace("\\", "/"),
                SavedFileName = tempFileName + extension,
                Size = new FileInfo(newFullPath).Length
            };
        }

        public FileDeleteResult Delete(string filePath)
        {
            File.Delete(filePath);
            return new FileDeleteResult() { Success = true };
        }

        public FileRenameResult Rename(string filePath, string newName, ActionPrompt promptType)
        {
            var dir = Path.GetDirectoryName(filePath);
            var extension = Path.GetExtension(filePath);
            var newPath = Path.Combine(dir, newName + extension);

            if (File.Exists(newPath) && promptType == ActionPrompt.DoNothing) return new FileRenameResult() { SelectedPromptType = promptType };
            if (File.Exists(newPath) && promptType == ActionPrompt.Replace) Delete(newPath);
            if (File.Exists(newPath) && promptType == ActionPrompt.AutoRename)
                newPath = CreateUniqueFullPath(filePath, newPath);

            File.Move(filePath, newPath);
            return new FileRenameResult()
            {
                SavedFilePath = "/" + newPath.Replace(AppDomain.CurrentDomain.BaseDirectory, "").Replace("\\", "/"),
                Success = true,
                SelectedPromptType = promptType
            };
        }

        public FileMoveResult Move(string filePath, string newPath, ActionPrompt promptType)
        {
            if (File.Exists(newPath) && promptType == ActionPrompt.DoNothing) return new FileMoveResult() { };
            if (File.Exists(newPath) && promptType == ActionPrompt.Replace) Delete(newPath);
            if (File.Exists(newPath) && promptType == ActionPrompt.AutoRename)
                newPath = CreateUniqueFullPath(filePath, newPath);

            File.Move(filePath, newPath);
            return new FileMoveResult() { Success = true, SelectedPromptType = promptType, SavedFilePath = newPath };
        }

        public FileCopyResult Copy(string filePath, string copyPath, ActionPrompt promptType)
        {
            if (File.Exists(copyPath) && promptType == ActionPrompt.DoNothing) return new FileCopyResult() { SelectedPromptType = promptType };
            if (File.Exists(copyPath) && promptType == ActionPrompt.Replace) Delete(copyPath);
            if (File.Exists(copyPath) && promptType == ActionPrompt.AutoRename)
                copyPath = CreateUniqueFullPath(filePath, copyPath);
            File.Copy(filePath, copyPath);

            return new FileCopyResult() { Success = true, SavedFilePath = copyPath, SelectedPromptType = promptType };
        }
        private string CreateUniqueFullPath(string path, string newPath)
        {
            int count = 0;
            var dir = Path.GetDirectoryName(newPath);
            var fileName = Path.GetFileNameWithoutExtension(path);
            var tempFileName = string.Empty;
            var ext = Path.GetExtension(path);
            while (File.Exists(newPath))
            {
                tempFileName = $"{fileName}({count++})";
                newPath = Path.Combine(dir, tempFileName + ext);
            }
            return newPath;
        }



    }
}