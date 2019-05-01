using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.Services.DataServices
{
    public class ProductCategoryDataService : CachedDataService<ProductCategory>, IProductCategoryDataService
    {
        public ProductCategoryDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override string[] CacheTags => new string[]{ ProductCategoryTag };
        public const string ProductCategoryTag = "ProductCategoryTag";


        public override Task<ProductCategory> GetAsync(Guid id)
        {
            return Query.Include(x => x.FieldDefenitions)
                .Include(x => x.FieldDefenitionGroups)
                .Include(x => x.FieldDefenitionGroups.Select(fdg => fdg.FieldDefenitions))
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public List<ProductCategory> GetAllCategoriesByParentProductCategories(Guid id)
        {
            var categories = Query.Where(x => x.ParentId == id).ToList();
            var childCategories=new List<ProductCategory>();
            foreach (var category in categories)
            {
                childCategories.AddRange(GetAllCategoriesByParentProductCategories(category.Id));
            }
            categories.AddRange(childCategories);
            return categories;
        }
    }
}
