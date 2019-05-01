using System;
using System.IO;
using System.Web.Http;
using System.Web.Mvc;
using DotLiquid;
using DotLiquid.NamingConventions;
using FluentScheduler;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;
using VarinaCmsV2.IocCofig;
using StructureMap;
using VarinaCmsV2.AutoMapperProfiles;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core;
using VarinaCmsV2.Core.Logic;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.Web.DependencyResolvers;
using VarinaCmsV2.Web.StartUpChainOfResoinsibility;

[assembly: OwinStartup(typeof(VarinaCmsV2.Web.Startup))]
namespace VarinaCmsV2.Web
{
    public partial class Startup
    {
        public static IContainer Container { get; } = AppObjectFactory.GetContainer();
        public void Configuration(IAppBuilder app)
        {

            RegisterOwinDepemdencies(app);
            ConfigureAuth(app);
            var config = new HttpConfiguration()
            {
                DependencyResolver = new WebApiDependecyResolver(Container),
                IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly,
            };
            app.UseWebApi(config);
            WebApiConfig.Register(config);
            DependencyResolver.SetResolver(new MvcDependencyResolver(Container));
            var initialDataHandler = new InitialDataHandler();
            initialDataHandler.Process(app, Container);
            //    new ExistingFolderFilesSeedDataBase().Seed(Container.GetInstance<IUnitOfWork>(), AppDomain.CurrentDomain.BaseDirectory + SettingHelper.UploadsBasePath);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            AutomapperConfiguration.Cofigure(Container);
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + SettingHelper.UploadsBasePath);
            Template.NamingConvention = new RubyNamingConvention();
            LiquidConfig.RegisterLiquidTagBlocks();
            Container.GetInstance<IUnitOfWork>().SeedDataBase();
            RegisterScheduledJobs();
        }

        private static void RegisterScheduledJobs()
        {
            JobManager.JobException += info =>
            {
                try
                {
                    TelegramMessenger.SendMessage(JsonConvert.SerializeObject(info.Exception) + "    " + info.Name);

                }
                catch (Exception e)
                {

                }
            };

            //JobManager.JobException += info =>
            //{

            //    try
            //    {
            //        ErrorSignal.Get().Raise(info.Exception);

            //    }
            //    catch (Exception e)
            //    {

            //    }
            //};
            InjectibleJob.Container = Container;
            JobManager.Initialize(new JobsRegistry());
        }
    }
}
