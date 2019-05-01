using DotLiquid;

namespace VarinaCmsV2.ViewModel.Eshop
{
    public class DownLoadFileLiquidAdapter:Drop
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
    }
}