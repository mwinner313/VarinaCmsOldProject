using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<VarinaCmsV2.Data.DbContexts.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(VarinaCmsV2.Data.DbContexts.AppDbContext context)
        {

            context.Set<Language>().AddOrUpdate(new Language() { Name = "fa-IR", ResourceAddress = "das" });
            context.Set<Language>().AddOrUpdate(new Language() { Name = "en-US", ResourceAddress = "das" });

            context.SaveChanges();



        }
    }
}
