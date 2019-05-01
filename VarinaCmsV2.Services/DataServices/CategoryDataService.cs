using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataBaseContracts;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Services.DataServices
{
    public class CategoryDataService : CachedDataService<Category>, ICategoryDataService
    {
        private readonly IEntitySchemeDataService _schemeDataService;

        public CategoryDataService(IUnitOfWork unitOfWork, IEntitySchemeDataService schemeDataService) : base(
            unitOfWork)
        {
            _schemeDataService = schemeDataService;
        }

        protected override string[] CacheTags => new string[] {CategoryTag};
        public const string CategoryTag = "CategoryTag";

        public async Task<Category> GetByUrlAndSchemeUrlAsync(string categoryUrl, string schemeUrl)
        {
            return await Query.FirstOrDefaultAsync(x => x.Scheme.Url == schemeUrl && x.Url == categoryUrl);
        }

        public override async Task AddAsync(Category model)
        {
            await base.AddAsync(model);
            model.Scheme = _schemeDataService.Query.First(x => x.Id == model.SchemeId);
        }
    }
}