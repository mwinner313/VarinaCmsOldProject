using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.IocCofig;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Security.Tests
{
    [TestClass]
    public class EmailServiceTests
    {
        [TestMethod]
        public async Task SendEmailTest()
        {
            var service = AppObjectFactory.GetContainer().GetInstance<IEmailSecuriyService>();

            await service.SendAsync(new IdentityMessage()
            {
                Destination = "m.ghanbari01375@gmail.com",
                Body = "hello",
                Subject = "testmessage"
            });

        }
    }
}
