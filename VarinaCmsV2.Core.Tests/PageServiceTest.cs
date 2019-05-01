using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.AutoMapperProfiles;
using VarinaCmsV2.Core.Logic.Services.Page;
using VarinaCmsV2.Core.Security;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Page;
using VarinaCmsV2.Core.Web.Security;
using VarinaCmsV2.IocCofig;
using VarinaCmsV2.Security;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.ViewModel.Page;

namespace VarinaCmsV2.Core.Tests
{
    [TestClass]
    public class PageServiceTest
    {
        [TestMethod]
        public async Task PageServiceAddTest()
        {
            //arrange 
          
            var userManager = AppObjectFactory.GetContainer().GetInstance<IAppUserManager>();
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(CustomClaimTypes.Premission, PagePremision.CanManage));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, (await userManager.FindByNameAsync("mohammadali")).Id.ToString()));
            var principal = new ClaimsPrincipal(identity);

            var pageAddRequest = new PageAddRequest()
            {
                RequestOwner = principal,
                ViewModel = new PageAddUpdateViewModel()
                {
                    Handle = "contactus",
                    UrlSegment = "/contactus",
                    Title = "تماس با ما",
                    LanguageName = "fa-IR",
                    Description = "test",
                    HtmlContent = "<h1>hello</h1>",
                    
                    
                }
            };
            AutomapperConfiguration.Cofigure(AppObjectFactory.GetContainer());
            var service = AppObjectFactory.GetContainer().GetInstance<IPageService>();
            var res = service.Add(pageAddRequest);
            Assert.AreEqual( (await res).Access,ResponseAccess.Granted);
        }
        [TestMethod]

        public async Task PageServiceAddWithParentTest()
        {
            //arrange 

            var userManager = AppObjectFactory.GetContainer().GetInstance<IAppUserManager>();
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(CustomClaimTypes.Premission, PagePremision.CanManage));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, (await userManager.FindByNameAsync("mohammadali")).Id.ToString()));
            var principal = new ClaimsPrincipal(identity);

            var pageAddRequest = new PageAddRequest()
            {
                RequestOwner = principal,
                ViewModel = new PageAddUpdateViewModel()
                {
                    Handle = "contactus",
                    UrlSegment = "/contactus",
                    Title = "تماس با ما",
                    LanguageName = "fa-IR",
                    Description = "test",
                    HtmlContent = "<h1>hello</h1>",
                    ParentId = Guid.Parse("5040e22d-739f-404a-9762-9da3f4ca7e22")

                }
            };
            AutomapperConfiguration.Cofigure(AppObjectFactory.GetContainer());
            var service = AppObjectFactory.GetContainer().GetInstance<IPageService>();
            var res = service.Add(pageAddRequest);
            Assert.AreEqual((await res).Access, ResponseAccess.Granted);
        }
    }
}
