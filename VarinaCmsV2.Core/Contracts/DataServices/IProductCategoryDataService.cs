﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.Core.Contracts.DataServices
{
    public interface IProductCategoryDataService:IDataService<ProductCategory,Guid>
    {
        List<ProductCategory> GetAllCategoriesByParentProductCategories(Guid id);
    }
}
