using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Owin;
using StructureMap;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Logic.DataSeeds;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Settings;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.Security.Contracts;
using WebGrease.Css.Extensions;

namespace VarinaCmsV2.Web.StartUpChainOfResoinsibility
{
    public class InitialDataHandler : IStartupHandler
    {
        public IStartupHandler Next { get; set; }
        public void Process(IAppBuilder app, IContainer container)
        {
            container.GetInstance<IAppUserManager>().SeedDatabase(new UserManagerSeedDataBaseContext());
            AddIntitalProvinces(container);
            var unitOfWork = container.GetInstance<IUnitOfWork>();
            AddInitialServiceMessageAccounts(unitOfWork);
            AddInitialWebSiteSettings(unitOfWork);
            Next?.Process(app, container);
        }

        private void AddInitialWebSiteSettings(IUnitOfWork unitOfWork)
        {
            var settings = unitOfWork.Set<Setting>();
            settings.AddOrUpdate(x => x.Name, new Setting()
            {
                Name = nameof(WebSiteBasicInformation),
                Icon = "info",
                RolesToAccess = $"{PreDefRoles.Administrator},{PreDefRoles.PrincipalAdministrator},{PreDefRoles.Developer}",
                TagsForSearch = "telegram facebook linkedin googleplus namasha aparat address نقشه map عنوان وبسایت توضیحات وبسایت تلگرام فیسبوک فیس بوک اینستاگرام گول پلاس اپارات آپارات نماشا مت تگ meta tag metatag کد آمار گیر گوگل کد امار گیر google analytics",
                DisplayName = "اطلاعات کلی وب سایت",
                Data = JObject.FromObject(new WebSiteBasicInformation()
                {
                    GoogleAnalytics = "<!-- کد آمار گیر  را به جای من وارد کن -->",
                })
            });
            settings.AddOrUpdate(x => x.Name, new Setting()
            {
                Icon = "shaparak",
                DisplayName = "تنظیمات درگاه پرداخت",
                RolesToAccess = $"{PreDefRoles.Administrator},{PreDefRoles.PrincipalAdministrator}",
                TagsForSearch = "بانک ",
                Name = nameof(ShaprakGateWaySetting),
                Data = JObject.FromObject(new ShaprakGateWaySetting()
                {

                })
            });
            settings.AddOrUpdate(x => x.Name, new Setting()
            {
                Icon = "politics",
                Name = nameof(WebSitePolicies),
                DisplayName = "سیاست های رفتاری وبسایت",
                RolesToAccess = $"{PreDefRoles.Administrator},{PreDefRoles.PrincipalAdministrator}",
                TagsForSearch = "کامنت comment نقش role ",
                Data = JObject.FromObject(new WebSitePolicies()
                {

                })
            });
            settings.AddOrUpdate(x => x.Name, new Setting()
            {
                DisplayName = "تنظیمات ادیتور محصولات",
                TagsForSearch = "اشانتیون محصولات مرتبط محصولات اجباری فایل دانلودی",
                RolesToAccess = $"{PreDefRoles.Supervisor}",
                Icon = "box",
                Name = nameof(WebSiteProductEditorSetting),
                Data = JObject.FromObject(new WebSiteProductEditorSetting()
                {

                })
            });
        }

        private void AddInitialServiceMessageAccounts(IUnitOfWork unitOfWork)
        {
            if (unitOfWork.Set<MessageServiceAccount>().All(x => x.Title != "varina")) { 
            unitOfWork.Set<MessageServiceAccount>().Add( new MessageServiceAccount
            {
                Title = "varina",
                MetaData = JObject.FromObject(new EmailInfo()
                {
                    Password = "vGoz18&1",
                    UserName = "test@varinaco.com",
                    Host = "mail.varinaco.com",
                    Port = 25,
                    IsSSlEnabled = false,
                    From = "test@varinaco.com"
                }),
                Type = "email",
                IsDefaultForUse = true
            });
                unitOfWork.SaveChanges();
            }
        }

        private void AddIntitalProvinces(IContainer container)
        {
            var unitOfWork = container.GetInstance<IUnitOfWork>();
            var dbset = unitOfWork.Set<StateProvince>();
            var prvinces = dbset.ToList();
            GetAvailibleStateProvinces().ForEach(x =>
            {
                var t = 1;
                var exiting = prvinces.FirstOrDefault(s => s.Name == x.Name);
                if (exiting == null)
                {
                    x.IsVisible = true;
                    unitOfWork.Add(x);
                }
            });
            container.GetInstance<IUnitOfWork>().SaveChanges();

        }

        private IEnumerable<StateProvince> GetAvailibleStateProvinces()
        {
            return new JavaScriptSerializer().Deserialize<List<StateProvince>>(
                File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/App_Data/Provinces.json"));
        }
    }
}