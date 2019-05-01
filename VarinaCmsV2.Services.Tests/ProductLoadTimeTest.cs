using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StructureMap;
using VarinaCmsV2.AutoMapperProfiles;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Logic.Web.UrlBuilding;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.IocCofig;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Services.Tests
{
    [TestClass]
    public class ProductLoadTimeTest
    {
        [TestMethod]
        public async Task LoadTimeTest()
        {
            var productDataService = AppObjectFactory.GetContainer().GetInstance<IProductDataService>();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var product = productDataService.Query.IncludeNecessaryData().Include(x => x.PrimaryCategory)
                .Include(x => x.PrimaryCategory.FieldDefenitions)
                .Include(x => x.PrimaryCategory.FieldDefenitionGroups)
                .Include(x => x.PrimaryCategory.FieldDefenitionGroups.Select(fdg => fdg.FieldDefenitions))
                .Include(x => x.Scheme)
                .Include(x => x.Scheme.FieldDefenitions)
                .Include(x => x.Scheme.FieldDefenitionGroups)
                .Include(x => x.Scheme.FieldDefenitionGroups.Select(fdg => fdg.FieldDefenitions)).FirstOrDefault();
            stopWatch.Stop();
            Console.Write(stopWatch.Elapsed + "elapsed");
        }
        [TestMethod]
        public async Task MappingTimeTest()
        {
            var productDataService = AppObjectFactory.GetContainer().GetInstance<IProductDataService>();

            var stopWatch = new Stopwatch();
            var product = productDataService.Query.IncludeNecessaryData().Include(x => x.PrimaryCategory)
                .Include(x => x.PrimaryCategory.FieldDefenitions)
                .Include(x => x.PrimaryCategory.FieldDefenitionGroups)
                .Include(x => x.PrimaryCategory.FieldDefenitionGroups.Select(fdg => fdg.FieldDefenitions))
                .Include(x => x.Scheme)
                .Include(x => x.Scheme.FieldDefenitions)
                .Include(x => x.Scheme.FieldDefenitionGroups)
                .Include(x => x.Scheme.FieldDefenitionGroups.Select(fdg => fdg.FieldDefenitions)).FirstOrDefault();
        
            AutomapperConfiguration.Cofigure(AppObjectFactory.GetContainer());


            stopWatch.Start();
            Mapper.Map<ProductViewModel>(product);
            stopWatch.Stop();
            Console.Write(stopWatch.Elapsed + " elapsed");
        }

        [TestMethod]
        public async Task LoadTimeByOptionsTest()
        {
            var productDataService = AppObjectFactory.GetContainer().GetInstance<IProductDataService>();

            AutomapperConfiguration.Cofigure(AppObjectFactory.GetContainer());

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var product = productDataService.Query.IncludeNecessaryData()
             .FirstOrDefault();
            await product.LoadDataByOptionsAsync(AppObjectFactory.GetContainer().GetInstance<IUnitOfWork>());
            stopWatch.Stop();
            Console.Write(stopWatch.Elapsed + " elapsed");
        }
    }
}
