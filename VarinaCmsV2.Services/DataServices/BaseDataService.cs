using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Services.DataServices
{
    public class BaseDataService<T> : IDataService<T, Guid> where T : class, IBaseEntity


    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IDbSet<T> DataSet;

        public BaseDataService(IUnitOfWork unitOfWork)
        {

            UnitOfWork = unitOfWork;
            DataSet = UnitOfWork.Set<T>();
        }

        public virtual IQueryable<T> Query => DataSet;

        public virtual async Task AddAsync(T model)
        {
            DataSet.Add(model);
            await UnitOfWork.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T model)
        {
            UnitOfWork.Update(model);
            await UnitOfWork.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            if (typeof(ISoftDeletibleItem).IsAssignableFrom(typeof(T)))
                await SoftDelete(id);
            else
            {
                var deleting = await DataSet.FirstAsync(x => x.Id == id);
                UnitOfWork.Delete(deleting);
                await UnitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                var deleteing = DataSet.First(x => x.Id == id);
                if (deleteing is ISoftDeletibleItem softDeletible)
                {
                    softDeletible.IsDeleted = true;
                    UnitOfWork.Update(softDeletible);
                }
                else
                {
                    var deleting = await DataSet.FirstAsync(x => x.Id == id);
                    UnitOfWork.Delete(deleting);
                }
            }
            await UnitOfWork.SaveChangesAsync();
        }

        private async Task SoftDelete(Guid id)
        {
            var dbentity = await DataSet.FirstOrDefaultAsync(x => x.Id == id);
            var softDeletible = dbentity as ISoftDeletibleItem;
            if (softDeletible != null) softDeletible.IsDeleted = true;
            UnitOfWork.Update(softDeletible);
            await UnitOfWork.SaveChangesAsync();
        }

        public virtual async Task<T> GetAsync(Guid id)
        {
            return await DataSet.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<TProjection> GetProjectToAsync<TProjection>(Guid id)
        {
            return DataSet.Where(x => x.Id == id).ProjectTo<TProjection>().FirstOrDefaultAsync();
        }

        public void Dispose()
        {
           
        }

        public void Attach(T model)
        {
            DataSet.Attach(model);
        }
    }
}