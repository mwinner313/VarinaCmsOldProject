using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.Data.DbContexts;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.IocCofig;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Tests.EntitiesTests
{
    [TestClass]
    public class UserTests
    {
        private readonly IAppUserManager _userManager = AppObjectFactory.GetContainer().GetInstance<IAppUserManager>();
        private readonly User _user = new User() {UserName = "test"};

        //[ClassCleanup]
        //private async Task ClearDatabase()
        //{
        //    var createdUser = await _userManager.FindByNameAsync(_user.UserName);
        //    await _userManager.DeleteAsync(createdUser.Id);
        //}
        [TestMethod]

        public async Task ShouldSaveAdditonalPremissionsDbTouched()
        {
          
            //act
            _user.AdditionalPermissions.Add(new Premission() { });

            //assert
            Assert.AreEqual(_user.AdditionalPermissions.Count, 1);
            await _userManager.CreateAsync(_user);

            var createdUser = await _userManager.FindByNameAsync(_user.UserName);
            Assert.AreEqual(createdUser.AdditionalPermissions.Count,1);
        }


    }
}
