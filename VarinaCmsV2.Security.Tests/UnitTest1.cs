using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StructureMap;
using VarinaCmsV2.IocCofig;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Security.Tests
{
    [TestClass]
    public class AppUserManagerTests
    {
        private readonly IContainer _container = AppObjectFactory.GetContainer();
        private readonly IAppUserManager _userManager;
        public AppUserManagerTests()
        {
            _userManager = _container.GetInstance<IAppUserManager>();
        }
        [TestMethod]
        public void ShouldReturnEmptyGuidForAnonymousUser()
        {
            Assert.AreEqual(Guid.Empty, _userManager.GetCurrentUserId());
        }
        [TestMethod]
        public void ShouldReturnNotEmptyGuidForLoggedInUser()
        {
            MockHttpContext();
            Assert.AreNotEqual(Guid.Empty, _userManager.GetCurrentUserId());
            Console.Write(_userManager.GetCurrentUserId());
        }
        void MockHttpContext()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter())
                );

            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));
            HttpContext.Current.User = new GenericPrincipal(identity, null);
        }
    }
}
