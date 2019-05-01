using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;

namespace VarinaCmsV2.Services.Helpers
{
    public static class DiscountHelper
    {
        public static List<ProductCategory> GetAllDiscountAppliedCategories (this Discount discount,IProductCategoryDataService productCategoryDataService)
        {
            if (discount.AppliedToSubCategories)
            {
                var parentCategories = productCategoryDataService.Query
                    .Where(x => x.AppliedDiscounts.Any(d => d.Id == discount.Id)).ToList();
                var childCategoris = new List<ProductCategory>();
                foreach (var parentCategory in parentCategories)
                    childCategoris.AddRange(
                        productCategoryDataService.GetAllCategoriesByParentProductCategories(parentCategory.Id));
                parentCategories.AddRange(childCategoris);
                return parentCategories;
            }

            discount.AppliedToCategories = productCategoryDataService.Query
                .Where(x => x.AppliedDiscounts.Any(d => d.Id == discount.Id)).ToList();
            return discount.AppliedToCategories.ToList();
        }
    }
}
