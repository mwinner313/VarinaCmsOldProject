using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Products
{
    public class SearchProductResult
    {
        public List<Product> Products { get; set; }
        public string SearchText { get; set; }
        public long Count { get; set; }
    }
}