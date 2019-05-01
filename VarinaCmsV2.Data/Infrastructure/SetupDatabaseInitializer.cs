using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Data.Infrastructure
{
    public class SetupDatabaseInitializer<TContext, TMigrationsConfiguration> : IDatabaseInitializer<TContext>
        where TContext : DbContext
        where TMigrationsConfiguration : DbMigrationsConfiguration<TContext>, new()
    {
        public void InitializeDatabase(TContext context)
        {
            var migrationConfig = CreateMigrationConfig(context);
            var migrator = new System.Data.Entity.Migrations.DbMigrator(migrationConfig);
            EnsureAllExplicitMigrationsAddedAndMigrate(context, migrator);
        }

        private void EnsureAllExplicitMigrationsAddedAndMigrate(TContext context, DbMigrator migrator)
        {
            var pendinMigrations = migrator.GetPendingMigrations().Count();
            var localMigrations = migrator.GetLocalMigrations().Count();
            var exists = context.Database.Exists();

            if (!exists || pendinMigrations > 0)
            {
                try
                {
                    migrator.Update();
                }
                catch (SqlException)
                {
                    throw new ApplicationException();
                }

                if (pendinMigrations == localMigrations)
                    Seed(context);
            }
        }

        private static TMigrationsConfiguration CreateMigrationConfig(TContext context)
        {
            return new TMigrationsConfiguration()
            {
                TargetDatabase =
                    new DbConnectionInfo(context.Database.Connection.ConnectionString, "System.Data.SqlClient")
            };
        }

        protected virtual void Seed(TContext context)
        {
        }
    }
}
