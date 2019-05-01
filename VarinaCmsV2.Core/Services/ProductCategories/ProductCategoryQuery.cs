using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Entities.Enums;

namespace VarinaCmsV2.Core.Services.ProductCategories
{
    public class ProductCategoryQuery
    {
        public bool MapForTreeView { get; set; }
        public CategoryType CategoryType { get; set; }
        public bool CheckByType { get; set; }
    }
}