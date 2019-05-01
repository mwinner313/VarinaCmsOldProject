using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using Z.EntityFramework.Plus;

namespace VarinaCmsV2.Services.DataServices
{
    public class FieldDefenitionDataService : CachedDataService<FieldDefenition>, IFieldDefenitionDataService
    {
        public FieldDefenitionDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override string[] CacheTags => new string[] { FieldDefenitionTag, EntitySchemeDataService.EntityCacheTag, ProductCategoryDataService.ProductCategoryTag, CategoryDataService.CategoryTag };

        public const string FieldDefenitionTag = "FieldDefenitionTag";

        public override Task<IEnumerable<FieldDefenition>> FromCacheAsync()
        {
            return DataSet.FromCacheAsync();
        }
    }
}
