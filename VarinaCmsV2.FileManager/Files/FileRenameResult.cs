namespace VarinaCmsV2.FileManager.Files
{
    public class FileRenameResult
    {
        /// <summary>
        /// virtual path to file starts with / ex: /Uploads/test.png
        /// </summary>
        public string SavedFilePath { get; set; }
        public bool Success { get; set; }
        public ActionPrompt SelectedPromptType { get; set; }
    }
}