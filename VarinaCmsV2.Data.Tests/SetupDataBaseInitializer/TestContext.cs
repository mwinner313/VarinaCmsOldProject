using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Data.Infrastructure;
using VarinaCmsV2.Data.Tests.Migrations;

namespace VarinaCmsV2.Data.Tests.SetupDataBaseInitializer
{
    public class TestContext:DbContext
    {
        public TestContext():base("name=DefaultConnection")
        {
            Database.SetInitializer(new SetupDatabaseInitializer<TestContext, Configuration>());
        }
        public DbSet<Test> Tests { get; set; }

    }

    public class Test
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
    }
}