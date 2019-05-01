using DotLiquid;

namespace VarinaCmsV2.ViewModel.Eshop
{
    public class ProductPictureLiquidViewModel:Drop 
    {
        public string Path { get; set; }
        public int DisplayOrder { get; set; }
        public string AltAttribute { get; set; }
        public string TitleAttribute { get; set; }
    }
}