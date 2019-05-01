using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using VarinaCmsV2.Data.Contracts;

namespace VarinaCmsV2.Services.Tests
{
    public class MockUnitOfWork : IUnitOfWork
    {
        public List<object> AddedData = new List<object>();
        public void Dispose()
        {
        }

        public void Add<T>(T entity) where T : class
        {
            AddedData.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
        }

        public void Detach<T>(T entity) where T : class
        {
        }

        public void Attach<T>(T entity) where T : class
        {
        }

        public void Update<T>(T entity) where T : class
        {
        }

        public async Task<int> SaveChangesAsync()
        {
            return 1;
        }

        public int SaveChanges()
        {
            return 1;
        }

        public DbSet<T> Set<T>() where T : class
        {
            return null;
        }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return null;
        }

        public void SeedDataBase()
        {
        }
    }
}
