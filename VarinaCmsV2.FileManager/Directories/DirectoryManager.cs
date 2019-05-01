using System;
using System.IO;
using VarinaCmsV2.FileManager.Files;

namespace VarinaCmsV2.FileManager.Directories
{
    public class DirectoryManager : IDirectoryManager
    {
        private readonly IFileManager _fileManager;

        public DirectoryManager(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public DirectorySaveResult Save(string path, ActionPrompt promptType)
        {
            if (Directory.Exists(path) && promptType == ActionPrompt.AutoRename)
                path = MakeItUniqe(path);

            Directory.CreateDirectory(path);
            return new DirectorySaveResult() { SavedPath = path, Success = true };
        }

        public DirectoryDeleteResult Delete(string path)
        {
            Directory.Delete(path);
            return new DirectoryDeleteResult() { Success = true };
        }

        public DirectoryRenameResult Rename(string path, string newPath, ActionPrompt promptType)
        {
            if (Directory.Exists(newPath) && promptType == ActionPrompt.AutoRename)
                newPath = MakeItUniqe(newPath);

            Directory.Move(path, newPath);
            return new DirectoryRenameResult() { Success = true, SavedPath = newPath };
        }

        public DirectoryMoveResult Move(string path, string newPath, ActionPrompt promptType)
        {
            if (Directory.Exists(newPath) && promptType == ActionPrompt.AutoRename)
                newPath = MakeItUniqe(newPath);

            Directory.Move(path, newPath);
            return new DirectoryMoveResult() { Success = true, SavedPath = newPath };
        }

        public DirectoryCopyResult Copy(string path, string copyPath, ActionPrompt promptType)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + path);
            }

            // If the destination directory does not exist, create it.
            if (Directory.Exists(copyPath)&&promptType==ActionPrompt.AutoRename)
            {
                copyPath = MakeItUniqe(copyPath);
                Directory.CreateDirectory(copyPath);
            }


            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                // Create the path to the new copy of the file.
                string temppath = Path.Combine(copyPath, file.Name);

                // Copy the file.
                _fileManager.Copy(file.FullName, temppath, promptType);
            }

            // If copySubDirs is true, copy the subdirectories.
            foreach (DirectoryInfo subdir in dirs)
            {
                // Create the subdirectory.
                string temppath = Path.Combine(copyPath, subdir.Name);

                // Copy the subdirectories.
                Copy(subdir.FullName, temppath, promptType);
            }
            return new DirectoryCopyResult() {Success = true,SavedPath = copyPath};
        }

        private string MakeItUniqe(string path)
        {
            int count = 0;
            while (Directory.Exists(path))
            {
                path = $"{path}({count++})";
            }
            return path;
        }
    }
}
