using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.Core.Contracts.DataBaseContracts;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel;
using VarinaCmsV2.ViewModel.Entities;
using Z.EntityFramework.Plus;

namespace VarinaCmsV2.Services.DataServices
{
    public abstract class CachedDataService<T> : BaseDataService<T>, IDataService<T, Guid> where T : BaseEntity

    {
        protected CachedDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected abstract string[] CacheTags { get; }

        public override async Task AddAsync(T model)
        {
            await base.AddAsync(model);
            QueryCacheManager.ExpireTag(CacheTags);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await base.DeleteAsync(id);
            QueryCacheManager.ExpireTag(CacheTags);
        }

        public override async Task UpdateAsync(T model)
        {
            await base.UpdateAsync(model);
            QueryCacheManager.ExpireTag(CacheTags);
        }

        public override async Task<T> GetAsync(Guid id)
        {
            return (await FromCacheAsync()).FirstOrDefault(x => x.Id == id);
        }
        public virtual Task<IEnumerable<T>> FromCacheAsync() => DataSet.FromCacheAsync(CacheTags);
    }
}
