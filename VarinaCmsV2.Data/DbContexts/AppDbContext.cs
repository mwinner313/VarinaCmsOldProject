using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.Data.Conventions;
using VarinaCmsV2.Data.DatabaseInitializers;
using VarinaCmsV2.Data.DbCommandInterceptors;
using VarinaCmsV2.Data.Migrations;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Data.DbContexts
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid, UserLogin, UserRole, UserClaim>, IUnitOfWork
    {
        private readonly List<GlobalFilter> _globalFilters;
        public AppDbContext(List<GlobalFilter> globalFilters = null) : base("name=DefaultConnection")
        {

            _globalFilters = globalFilters;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());
#if DEBUG
            Action<string> print = message => Debug.WriteLine(message);
            this.Database.Log = print;
#endif
           Database.SetInitializer<AppDbContext>(null);
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppDbContext>());
        }
        public AppDbContext() : base("name=DefaultConnection")
        {
          //  Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());
        }

        static AppDbContext()
        {
            DbInterception.Add(new YeKeInterceptor());
        }

        public void Update<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Modified;

        }

        public void Delete<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public void Add<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Added;
        }

        public void Detach<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Detached;
        }
        public void Attach<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Unchanged;
        }

        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public void SeedDataBase()
        {
            var emailAccount = new MessageServiceAccount
            {
                Title = "varina",
                MetaData = JObject.FromObject(new EmailInfo()
                {
                    Password = "vGoz18&1",
                    UserName = "test@varinaco.com",
                    Host = "mail.varinaco.com",
                    Port = 25,
                    IsSSlEnabled = true,
                    From = "test@varinaco.com"
                }),
                Type = "email",
                IsDefaultForUse = true
            };
            Set<MessageServiceAccount>().AddOrUpdate(x => x.Title, emailAccount);
            SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            try
            {
                var updated = ChangeTracker.Entries<BaseEntity>().Where(x => x.State == EntityState.Modified).ToList();
                var entries = ChangeTracker.Entries<BaseEntity>().Where(x => x.State == EntityState.Added).ToList();
                foreach (var entry in entries)
                {
                    if (entry.Entity.Id != Guid.Empty && !(entry.Entity is IHaveOneToOneRelation)) Entry(entry.Entity).State = EntityState.Detached;
                }
                return await base.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                throw;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<MapProtectedFieldConvention>();
            modelBuilder.Configurations.AddFromAssembly(typeof(User).Assembly);
            modelBuilder.Ignore<BaseEntity>();
            FindAndRegisterEntitiesFromAssembly(typeof(BaseEntity).Assembly, typeof(DbEntity), modelBuilder);
            _globalFilters?.ForEach(x => x.ApplyFilter(modelBuilder));
            base.OnModelCreating(modelBuilder);
        }

        private void FindAndRegisterEntitiesFromAssembly(Assembly assembly, Type type, DbModelBuilder modelBuilder)
        {
            assembly
                .GetTypes()
                .Where(x => type.IsAssignableFrom(x) && type != x)
                .ToList()
                .ForEach(modelBuilder.RegisterEntityType);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}