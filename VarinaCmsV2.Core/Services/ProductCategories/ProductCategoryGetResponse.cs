namespace VarinaCmsV2.Core.Services.ProductCategories
{
    public class ProductCategoryGetResponse : IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
    }
}
