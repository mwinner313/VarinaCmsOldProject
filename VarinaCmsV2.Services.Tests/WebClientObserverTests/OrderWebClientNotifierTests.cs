using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.IocCofig;
using VarinaCmsV2.Services.WebClientObservers.Orders;

namespace VarinaCmsV2.Services.Tests.WebClientObserverTests
{
    [TestClass]
    public class OrderWebClientNotifierTests
    {
        [TestMethod]
        public async Task ShouldNotifyAdminstrators()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter())
            );

            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));
            HttpContext.Current.User = new GenericPrincipal(identity, null);

            await AppObjectFactory.GetContainer().GetInstance<WebSiteOwnerOrderNotifier>()
                .OrderPaid(new Order() {Id = Guid.NewGuid()});
        }

        [TestMethod]
        public void TestTelegram()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter())
            );
            TelegramMessenger.SendMessage("empty email address to send product inventory info");
        }
    }
}
