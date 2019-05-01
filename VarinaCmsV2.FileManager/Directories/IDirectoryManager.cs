
namespace VarinaCmsV2.FileManager.Directories
{
    public interface IDirectoryManager
    {
        DirectorySaveResult Save(string path,ActionPrompt promptType);
        DirectoryDeleteResult Delete(string path);
        DirectoryRenameResult Rename(string path, string newPath, ActionPrompt promptType);
        DirectoryMoveResult Move(string path, string newPath, ActionPrompt promptType);
        DirectoryCopyResult Copy(string path, string copyPath, ActionPrompt promptType);
    }
}
