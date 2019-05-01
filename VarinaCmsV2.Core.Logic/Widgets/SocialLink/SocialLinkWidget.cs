using AutoMapper;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Core.Contracts.Widget;
using VarinaCmsV2.Core.Services.Settings;
using VarinaCmsV2.Core.Settings;
using VarinaCmsV2.ViewModel;

namespace VarinaCmsV2.Core.Logic.Widgets.SocialLink
{
    internal class SocialLinkWidget : BaseWidget
    {
        private readonly ISettingService _settingService;

        public SocialLinkWidget()
        {
            _settingService = Container.GetInstance<ISettingService>();
        }

        public override string Type => "social_link";

        public override BaseWidgetLiquidData Parse(JObject widgetMetaData)
        {
           
            return Mapper.Map<SocialLinks>(_settingService.GetSetting<WebSiteBasicInformation>());
        }
    }
    
}