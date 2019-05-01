using System;
using System.Collections.Generic;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Core.Services.Product
{
    public class ProductListResponse : IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public long Count { get; set; }
    }
}