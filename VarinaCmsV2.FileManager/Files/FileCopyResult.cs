namespace VarinaCmsV2.FileManager.Files
{
    public class FileCopyResult
    {
        public string SavedFilePath { get; set; }
        public bool Success { get; set; }
        public ActionPrompt SelectedPromptType { get; set; }
    }
}