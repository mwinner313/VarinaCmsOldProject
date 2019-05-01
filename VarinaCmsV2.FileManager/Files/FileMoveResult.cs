namespace VarinaCmsV2.FileManager.Files
{
    public class FileMoveResult
    {
        public string SavedFilePath { get; set; }
        public bool Success { get; set; }
        public ActionPrompt SelectedPromptType { get; set; }
    }
}