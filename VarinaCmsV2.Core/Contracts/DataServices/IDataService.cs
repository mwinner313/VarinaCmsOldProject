using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Contracts.DataServices
{
    public interface IDataService<T, TKey>:IDisposable where T :class 
    {
        Task AddAsync(T model);
        void Attach(T model);
        Task UpdateAsync(T model);
        Task<TProjection> GetProjectToAsync<TProjection>(TKey id);
        Task DeleteAsync(TKey id);
        Task DeleteAsync(List<TKey> ids);
        Task<T> GetAsync(TKey id);
        IQueryable<T> Query { get; }
    }
}
