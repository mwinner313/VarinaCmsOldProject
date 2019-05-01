using System;

namespace VarinaCmsV2.Core.Services.Category
{
    public class CategoryParentChildUpdateRequest:ParentChildUpdateRequest
    {
        public Guid SchemeId { get; set; }
    }
}