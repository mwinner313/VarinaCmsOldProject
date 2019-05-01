using VarinaCmsV2.Common;

namespace VarinaCmsV2.Core.Services.File
{
    public class FileQuery:Pagenation
    {
        public string SearchText { get; set; }
        public string Extension { get; set; }
    }
}