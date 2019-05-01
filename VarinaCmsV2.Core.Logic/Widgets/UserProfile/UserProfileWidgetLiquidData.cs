using VarinaCmsV2.Core.Contracts.Widget;
using VarinaCmsV2.ViewModel.User.Account;

namespace VarinaCmsV2.Core.Logic.Widgets.UserProfile
{
    internal class UserProfileWidgetLiquidData : BaseWidgetLiquidData
    {
        public UserLiquidViewModel user { get; set; }
    }
}