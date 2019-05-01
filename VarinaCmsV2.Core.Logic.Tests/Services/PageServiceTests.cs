using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.AutoMapperProfiles;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Page;
using VarinaCmsV2.IocCofig;
using VarinaCmsV2.Security;
using VarinaCmsV2.ViewModel.Page;

namespace VarinaCmsV2.Core.Logic.Tests.Services
{
    [TestClass]
    public class PageServiceTests
    {
        private readonly IPageService _pageService = AppObjectFactory.GetContainer().GetInstance<IPageService>();

        public PageServiceTests()
        {
            AutomapperConfiguration.Cofigure(AppObjectFactory.GetContainer());
        }
        [TestMethod]
        public async Task ShouldDenyAddNewPage()
        {
            var identity = new ClaimsIdentity();
            Thread.CurrentPrincipal = new ClaimsPrincipal(identity);

            var res = await _pageService.Add(new PageAddRequest()
            {
                RequestOwner = Thread.CurrentPrincipal,
                ViewModel = new PageAddUpdateViewModel()
                {
                    HtmlContent = "test",
                    UrlSegment = "1 2 4  d d",
                    LanguageName = "fa-IR"
                }
            });

            Assert.AreEqual(res.Access, ResponseAccess.Deny);
        }
        [TestMethod]
        public async Task ShouldGrantAddNewPage()
        {
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(CustomClaimTypes.Premission, PagePremision.CanSee));
            identity.AddClaim(new Claim(CustomClaimTypes.Premission, PagePremision.CanManage));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "12ea9e35-6b19-44d2-981c-2a8a3e6318e7"));
            Thread.CurrentPrincipal = new ClaimsPrincipal(identity);

            var res = await _pageService.Add(new PageAddRequest()
            {
                RequestOwner = Thread.CurrentPrincipal,
                ViewModel = new PageAddUpdateViewModel()
                {
                    HtmlContent = "test",
                    UrlSegment = "1 2 4  d d",
                    LanguageName = "fa-IR",
                    Handle = "t",
                    Title = "contact"
                }
            });

            Assert.AreEqual(res.Access, ResponseAccess.Granted);
        }
        [TestMethod]
        public async Task ShouldDenyListPageRequest()
        {
            var identity = new ClaimsIdentity();

            Thread.CurrentPrincipal = new ClaimsPrincipal(identity);

            var res = await _pageService.Get(new PageListRequest()

            {

                RequestOwner = Thread.CurrentPrincipal,
                Query = new PageQuery()
                {
                    LanguageName = "fa-IR",
                    PageSize = 15,
                    PageNumber = 2,
                    SearchText = "hello"
                }

            });

            Assert.AreEqual(res.Access, ResponseAccess.Deny);
        }

        [TestMethod]
        public async Task ShouldGrantListPageRequest()
        {
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(CustomClaimTypes.Premission, PagePremision.CanSee));
            Thread.CurrentPrincipal = new ClaimsPrincipal(identity);
            var res = await _pageService.Get(new PageListRequest()
            {
                RequestOwner = Thread.CurrentPrincipal,
                Query = new PageQuery()
                {
                    LanguageName = "en-US",
                    PageSize = 15,
                    PageNumber = 2,
                    SearchText = "seo"
                }
            });

            Assert.AreEqual(res.Access, ResponseAccess.Granted);
            Assert.AreEqual(res.Pages.Count, 15);
            Assert.AreEqual(res.Pages.Any(x=>x.IsDeleted),false);
        }

         [TestMethod]
        public async Task ShouldDenyDeletePageRequest()
        {
            var identity = new ClaimsIdentity();
            Thread.CurrentPrincipal = new ClaimsPrincipal(identity);
            var res = await _pageService.Delete(new PageDeleteRequest()
            {
                PageId = Guid.Parse("7412559E-3870-E711-BDA9-B8EE65D632F4"),
                RequestOwner = Thread.CurrentPrincipal
            });
            Assert.AreEqual(res.Access, ResponseAccess.Deny);
        }
        [TestMethod]
        public async Task ShouldGrantDeletePageRequest()
        {
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(CustomClaimTypes.Premission, PagePremision.CanManage));
            Thread.CurrentPrincipal = new ClaimsPrincipal(identity);
            var res = await _pageService.Delete(new PageDeleteRequest()
            {
                PageId = Guid.Parse("7412559E-3870-E711-BDA9-B8EE65D632F4"),
                RequestOwner = Thread.CurrentPrincipal
            });
            Assert.AreEqual(res.Access, ResponseAccess.Granted);
        }
    }
}
