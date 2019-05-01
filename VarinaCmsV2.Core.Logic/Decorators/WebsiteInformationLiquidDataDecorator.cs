using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using DotLiquid;
using Microsoft.AspNet.Identity;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.Decorators;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Services.Settings;
using VarinaCmsV2.Core.Settings;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.ViewModel.Settings;

namespace VarinaCmsV2.Core.Logic.Decorators
{
    class WebsiteInformationLiquidDataDecorator: LiquidDataDecorator
    {
        private readonly Drop _wrappe;

        public WebsiteInformationLiquidDataDecorator()
        {
        }

        public WebsiteInformationLiquidDataDecorator(Drop wrappe)
        {
            _wrappe = wrappe;
        }

        public override int LevelToReachRealWrappe { get; } = 5;

        public override bool ShouldDecorate(Drop wrappe)
        {
            return true;
        }

        public override LiquidAdapter Decorate(Drop wrapee)
        {
            return new WebsiteInformationLiquidDataDecorator(wrapee) { Container = Container };
        }

        public override object BeforeMethod(string method)
        {
            if (method == "website_info") return HandleMethod();
            return _wrappe != null ? _wrappe.BeforeMethod(method) : base.BeforeMethod(method);
        }

        private object HandleMethod()
        {
            return Mapper.Map<WebSiteBasicInformationLiquidVeiwModel>(Container.GetInstance<ISettingService>()
                .GetSetting<WebSiteBasicInformation>());
        }
    }
}
