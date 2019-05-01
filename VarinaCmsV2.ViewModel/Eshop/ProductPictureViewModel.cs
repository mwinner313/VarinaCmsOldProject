using System;

namespace VarinaCmsV2.ViewModel.Eshop
{
    public class ProductPictureViewModel:BaseVeiwModel
    {
        public string Path { get; set; }
        public Guid ProductId { get; set; }
        public int DisplayOrder { get; set; }
        public string AltAttribute { get; set; }
        public string TitleAttribute { get; set; }
    }
}