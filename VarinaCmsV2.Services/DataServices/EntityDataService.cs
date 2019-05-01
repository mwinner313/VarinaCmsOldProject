using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Services.DataServices
{
    public class EntityDataService : BaseDataService<Entity>, IEntityDataService
    {
        public EntityDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override Task<Entity> GetAsync(Guid id)
        {
            return Query
                .Include(x => x.PrimaryCategory)
                .Include(x => x.PrimaryCategory.FieldDefenitions)
                .Include(x => x.PrimaryCategory.FieldDefenitionGroups)
                .Include(x => x.PrimaryCategory.FieldDefenitionGroups.Select(fdg=>fdg.FieldDefenitions))
                .Include(x => x.Fields)
                .Include(x => x.Scheme)
                .Include(x => x.RelatedCategories)
                .Include(x => x.Tags)
                .Include("Fields.FieldDefenition").FirstAsync(x => x.Id == id);
        }

      
    }
}
