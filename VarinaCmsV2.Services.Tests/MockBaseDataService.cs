using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Services.Tests
{
    public class MockBaseDataService<T>:IDataService<T,Guid> where T:BaseEntity
    {
        protected readonly List<T> Items=new List<T>();
        public void Dispose()
        {
           
        }

        public async Task AddAsync(T model)
        {
            model.Id = Guid.NewGuid();
            Items.Add(model);
        }

        public void Attach(T model)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T model)
        {
            throw new NotImplementedException();
        }

        public Task<TProjection> GetProjectToAsync<TProjection>(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = Items.FirstOrDefault(x => x.Id == id);
            Items.Remove(item);
        }

        public Task DeleteAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAsync(Guid id)
        {
            return Items.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<T> Query => Items.AsQueryable();
    }
}
