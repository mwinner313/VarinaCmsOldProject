using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.Utilities;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper.QueryableExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.AutoMapperProfiles;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Logic.Services.Menu;
using VarinaCmsV2.Core.Services.Menu;
using VarinaCmsV2.DomainClasses;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.IocCofig;
using VarinaCmsV2.ViewModel.Menu;
using Z.EntityFramework.Plus;

namespace VarinaCmsV2.Core.Logic.Tests.Services
{
    /// <summary>
    /// Summary description for MenuServiceTests
    /// </summary>
    [TestClass]
    public class MenuServiceTests
    {
        private readonly IMenuService _service = AppObjectFactory.GetContainer().GetInstance<IMenuService>();
        private readonly IMenuDataService _menuDataService = AppObjectFactory.GetContainer().GetInstance<IMenuDataService>();
        private readonly IPageDataService _pageDataService = AppObjectFactory.GetContainer().GetInstance<IPageDataService>();
        private readonly ICategoryDataService _categoryDataService = AppObjectFactory.GetContainer().GetInstance<ICategoryDataService>();
        private readonly IEntityDataService _entityData = AppObjectFactory.GetContainer().GetInstance<IEntityDataService>();
        private readonly IUserDataService _userData = AppObjectFactory.GetContainer().GetInstance<IUserDataService>();
        public MenuServiceTests()
        {

            AutomapperConfiguration.Cofigure(AppObjectFactory.GetContainer());
        }



        [TestMethod]
        public async Task ShouldMapUrls()
        {
            //     HttpContext.Current = new HttpContext(
            //new HttpRequest("", "http://tempuri.org", "") {Headers = {["X-Panel-Request"]="true"}},
            //new HttpResponse(new StringWriter())
            //);

            await _menuDataService.AddAsync(new Menu()
            {
                Title = "testMenu",
                LanguageName = "fa-IR"
 ,
                MenuItems = new List<MenuItem>()
                {
                    new MenuItem() {Title = "page1",TargetId =_pageDataService.Query.First().Id,TargetType = MenuItemTargetType.Page,LanguageName = "fa-IR"},
                    new MenuItem() {Title = "category1",TargetType = MenuItemTargetType.Category,TargetId = _categoryDataService.Query.First().Id,LanguageName = "fa-IR"},
                    new MenuItem() {Title = "custom1",TargetType = MenuItemTargetType.CustomLink,Url = "www.google.com",LanguageName = "fa-IR"},
                    new MenuItem() {Title = "entity1",TargetId = _entityData.Query.First().Id,TargetType = MenuItemTargetType.Entity,LanguageName = "fa-IR"},
                    new MenuItem() {Title = "user1",TargetType = MenuItemTargetType.UserProfile,TargetId = _userData.Query.First().Id,LanguageName = "fa-IR"}
                }
            });

            //var menus=   _menuDataService.Query.Include(x=>x.MenuItems).ToList();
            //foreach (var menu in menus)
            //{
            //    foreach (var menuiItem in menu.MapToViewModel().MenuItems)
            //    {
            //        Console.WriteLine(menuiItem.Url);
            //    }
            //}
            //await _menuDataService.Query.Where(x => x.Title == "testMenu").DeleteAsync();
        }
    }
}
