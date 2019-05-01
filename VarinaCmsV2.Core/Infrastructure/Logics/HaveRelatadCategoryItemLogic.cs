using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using StructureMap.Attributes;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.Core.Infrastructure.Logics
{
    public class HaveRelatadCategoryItemLogic : IBaseBeforeAddingEntityLogic, IBaseBeforeUpdatingEntityLogic
    {
        [SetterProperty]
        public IContainer Container { get; set; }
        public async Task ApplyLogicAsync<T>(T obj, IPrincipal requestOwner) where T : class 
        {
            if (obj is Product product)
            {
                var unitOfWork = Container.GetInstance<IUnitOfWork>();
                var categories = unitOfWork.Set<ProductCategory>();
                var recevingCategories = product.RelatedCategories.ToList();
                product.RelatedCategories.Clear();
                foreach (var rCategory in recevingCategories)
                {
                    var category = categories.First(x => x.Id == rCategory.Id);
                    product.RelatedCategories.Add(category);
                }
            }
            if (obj is Entity entity)
            {
                var item = (IHaveRelatedCategoryItem<Category>)entity;

                var unitOfWork = Container.GetInstance<IUnitOfWork>();
                var categories = unitOfWork.Set<Category>();
                var recevingCategories = item.RelatedCategories.ToList();
                item.RelatedCategories.Clear();
                foreach (var rCategory in recevingCategories)
                {
                    var category = categories.First(x => x.Id == rCategory.Id);
                    item.RelatedCategories.Add(category);
                }
            }

          
        }
    }
}
