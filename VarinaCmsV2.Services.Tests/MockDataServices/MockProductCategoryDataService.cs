using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Services.ProductCategories;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;

namespace VarinaCmsV2.Services.Tests.MockDataServices
{
    public class MockProductCategoryDataService : MockBaseDataService<ProductCategory>, IProductCategoryDataService
    {

        public Guid ClothesCatId { get; } = Guid.NewGuid();
        public Guid ElectronicsCatId { get; } = Guid.NewGuid();
        public Guid KidsCatId { get; set; } = Guid.NewGuid();
        public Guid GlovesCatId { get; set; } = Guid.NewGuid();
        public Guid ChirstMasGlovesCatId { get; set; } = Guid.NewGuid();

        public MockProductCategoryDataService()
        {
            Items.AddRange(GetCategories());
        }

        private IEnumerable<ProductCategory> GetCategories()
        {
            var cat1 = new ProductCategory() { Id = ClothesCatId, Title = "لباس", Childs = new List<ProductCategory>() ,AppliedDiscounts = new List<Discount>()};
            var cat2 = new ProductCategory() { Id = ElectronicsCatId, Title = "کالای دیجیتال", AppliedDiscounts = new List<Discount>() };
            var cat3 = new ProductCategory() { Id = KidsCatId, Title = "کودک و نوجوان", AppliedDiscounts = new List<Discount>() };
            var cat4 = new ProductCategory() { Id = GlovesCatId, Title = "دست کش", ParentId = ClothesCatId, Childs = new List<ProductCategory>(), AppliedDiscounts = new List<Discount>() };
            var cat5 = new ProductCategory() { Id = ChirstMasGlovesCatId, Title = "دست کش کریسمس", Childs = new List<ProductCategory>() ,ParentId = GlovesCatId, AppliedDiscounts = new List<Discount>() };
            cat1.Childs.Add(cat4);
            cat4.Childs.Add(cat5);
            return new List<ProductCategory>()
           {
               cat1,
               cat2,
               cat3,
               cat4,
               cat5
           };
        }


        public List<ProductCategory> GetAllCategoriesByParentProductCategories(Guid id)
        {
            var categories = Query.Where(x => x.ParentId == id).ToList();
            var childCategories = new List<ProductCategory>();
            foreach (var category in categories)
            {
                childCategories.AddRange(GetAllCategoriesByParentProductCategories(category.Id));
            }
            categories.AddRange(childCategories);
            return categories;
        }
    }
}
