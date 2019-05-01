using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Data.Contracts;

namespace TestCommon
{
    public class MockUnitOfWork : IUnitOfWork
    {
        public List<object> AddedData = new List<object>();
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Add<T>(T entity) where T : class
        {
            AddedData.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Detach<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Attach<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void SeedDataBase()
        {
            throw new NotImplementedException();
        }
    }
}
