﻿using System.Collections.Generic;
using VarinaCmsV2.ViewModel.Category;

namespace VarinaCmsV2.Core.Services.Category
{

    public class CategoryListResponse:IServiceResponse
    {
        public List<CategoryViewModel> Categories { get; set; }
        public ResponseAccess Access { get; set; }
        public string Message { get; set; } = "";
        public long Count { get; set; }
    }
}