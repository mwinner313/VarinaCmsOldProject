using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Products
{
    public class PaginatedProducTag
    {
        public Tag Tag { get; set; }
        public List<Product> Products { get; set; }
        public int CurrentPage { get; set; }
        public int AllPagesCount { get; set; }
    }
}