using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using Z.EntityFramework.Plus;

namespace VarinaCmsV2.Services.DataServices
{
    public class EntitySchemeDataService : CachedDataService<EntityScheme>, IEntitySchemeDataService
    {
        public const string EntityCacheTag = "EntityCacheTag";

        public EntitySchemeDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override string[] CacheTags => new[] {EntityCacheTag};

        public override Task<IEnumerable<EntityScheme>> FromCacheAsync()
        {
            return Query
                .Include(x => x.FieldDefenitions)
                .Include(x => x.FieldDefenitionGroups)
                .Include(x => x.FieldDefenitionGroups.Select(fdg => fdg.FieldDefenitions))
                .FromCacheAsync(CacheTags);
        }

        public override Task<EntityScheme> GetAsync(Guid id)
        {
            return Query
                .Include(x => x.FieldDefenitions)
                .Include(x => x.FieldDefenitionGroups)
                .Include(x => x.FieldDefenitionGroups.Select(fdg => fdg.FieldDefenitions))
                .FirstAsync(x => x.Id == id);
        }
    }
}