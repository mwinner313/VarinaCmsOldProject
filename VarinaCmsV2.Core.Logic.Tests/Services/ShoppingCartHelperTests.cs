using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.IocCofig;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.Services.WebClientServices;

namespace VarinaCmsV2.Core.Logic.Tests.Services
{
    [TestClass]
    public class ShoppingCartHelperTests
    {
        [TestMethod]
        public async Task ShouldHandleNewItem()
        {
            var helper = new ShoppingCartHelper();

            var unitOfWork = AppObjectFactory.GetContainer().GetInstance<IUnitOfWork>();
            await helper.AddToCartAsync(unitOfWork.Set<Product>().First(), 2, unitOfWork.Set<User>().First(),
                unitOfWork);
        }
       
    }
}
