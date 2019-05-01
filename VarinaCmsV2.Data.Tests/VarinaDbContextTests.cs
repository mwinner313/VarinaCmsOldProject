using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Data.DbContexts;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Data.Tests
{
    [TestClass]
    public class VarinaDbContextTests
    {
        [TestMethod]
        public void ShoudSaveNewData()
        {
            var context = new AppDbContext();
            var set = context.Set<MessageServiceAccount>();

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
            set.Add(emailAccount);
            context.SaveChanges();
        }


    }
}
