using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.Common;
using VarinaCmsV2.Configuration.Base;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.Infrastructure.GlobalFilters;
using VarinaCmsV2.Data.DbContexts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.Enums;

namespace VarinaCmsV2.Core.Tests.GlobalFilterTests
{
    [TestClass]
    public class GlobalizedItemGlobalFilterTests
    {
        private readonly AppDbContext _db = new AppDbContext(new List<GlobalFilter>()
        {
            new GlobalizeItemGlobalFilter() 
        });

        [TestCleanup]
        void DeleteData()
        {
         
        }

        //[TestInitialize]
        //public void MockData()
        //{
        //    for (int i = 0; i < 1000; i++)
        //    {
        //        _db.Set<Category>().Add(new Category()
        //        {
        //            Handle = "test" + i,
        //            AccessMode = AccessMode.Public,
        //            CatigoryType = i % 2 == 1 ? CatigoryType.Mixed : CatigoryType.Main,
        //            LanguageName = i % 2 == 1 ? "fa-IR" : "en-US",
        //            UrlSegment = new UrlBuilder().GenrateUrlSegment("test" + i),
        //            Url = new UrlBuilder().GenrateUrlSegment("test" + i),
        //            Title = "test" + i,
        //            SchemeId = _db.Set<EntityScheme>().First().Id
        //        });
        //    }
        //    _db.SaveChanges();
        //}

        [TestMethod]
        public void ShouldFilterDataBasedOnLanguageName()
        {
            HttpContext.Current = new HttpContext(
                  new HttpRequest("", "http://tempuri.org", ""),
                  new HttpResponse(new StringWriter())
                  );
            Console.WriteLine(_db.Set<Category>().Count());
        }
    }
}
