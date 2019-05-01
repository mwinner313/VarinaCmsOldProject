using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace VarinaCmsV2.Data.Contracts
{
    public interface IUnitOfWork:IDisposable
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Detach<T>(T entity) where T : class;
        void Attach<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class ;
        Task<int> SaveChangesAsync();
        int SaveChanges();
        DbSet<T> Set<T>() where T : class ;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        void SeedDataBase();
    }
}