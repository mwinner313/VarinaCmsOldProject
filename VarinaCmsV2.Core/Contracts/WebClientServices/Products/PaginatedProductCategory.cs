using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Products
{
    public class PaginatedProductCategory
    {
        public ProductCategory ProductCategory { get; set; }
        public List<Product> Products { get; set; }
        public int CurrentPage { get; set; }
        public int AllPagesCount { get; set; }

    }
}