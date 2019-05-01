using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace VarinaCmsV2.FileManager.Files
{
    public interface IFileManager
    {
        FileSaveResult Save(HttpPostedFileBase file, string fullPath = null);
        Task<FileSaveResult> SaveAsync(HttpContent file, string directoryPath );
        FileDeleteResult Delete(string filePath);
        FileRenameResult Rename(string filePath, string newName, ActionPrompt promptType);
        FileMoveResult Move(string filePath, string newPath, ActionPrompt promptType);
        FileCopyResult Copy(string filePath, string copyPath, ActionPrompt promptType);

    }
}
