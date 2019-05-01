using System.Data.Entity;
using System.Linq;
using DotLiquid;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Widget;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Logic.Widgets.UserProfile
{
    internal class UserProfileWidget : BaseWidgetWithMetaData<UserProfileMetaData>
    {
        private readonly IUserDataService _userDataService;
        public UserProfileWidget()
        {
            _userDataService = Container.GetInstance<IUserDataService>();
        }
        public override string Type { get; } = "user-profile";
   

        public override BaseWidgetLiquidData Parse(JObject widgetMetaData)
        {
           base.DeserializeMetaData(widgetMetaData);
            var user = _userDataService.Query
                .Include(x => x.Followers)
                .Include(x => x.Following)
                .Include(x => x.CreatedEntities).First(x => x.Id == MetaData.UserId);
            return new UserProfileWidgetLiquidData() { user = user.AsLiquidAdapted() };
        }
    }
}