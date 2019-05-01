using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using VarinaCmsV2.Core.Contracts.Widget;
using VarinaCmsV2.Core.Settings;
using VarinaCmsV2.ViewModel.Settings;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class CommonProfile:Profile
    {
        public CommonProfile()
        {
            CreateMap<WebSiteBasicInformation, SocialLinks>();
            CreateMap<WebSiteBasicInformation, WebSiteBasicInformationLiquidVeiwModel>();
        }
    }
}
