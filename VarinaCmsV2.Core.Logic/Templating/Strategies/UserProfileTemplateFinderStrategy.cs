using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Logic.Templating.Strategies
{
    public class UserProfileTemplateFinderStrategy: ILiquidTemplateFinderStrategy
    {

        public UserProfileTemplateFinderStrategy()
        {
          
        }

        public string GetTemplatePathDefault(ITemplatedItem item)
        {
            return $"user-profile";
        }

        public string GetTemplatePathByConvention(ITemplatedItem item)
        {
           return $"UserProfiles/user-profile-{item.Handle}";
        }
    }
}
