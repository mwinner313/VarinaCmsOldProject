using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.Core.Logic.Templating.Exceptions;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.DTO;

namespace VarinaCmsV2.Core.Logic.Templating.Strategies
{
    public class UserCreatedEntityTemplateFinderStrategy : ILiquidTemplateFinderStrategy
    {

        public UserCreatedEntityTemplateFinderStrategy()
        {
        }
        public string GetTemplatePathDefault(ITemplatedItem item = null)
        {
            if (!(item is UserEntitiesPaginatedLiquidAdapted adapted)) throw new InvalidITempatedItemException(item, this);
            return $"User/user-{adapted.Scheme.Handle}";
        }

        public string GetTemplatePathByConvention(ITemplatedItem item = null)
        {
            if (!(item is UserEntitiesPaginatedLiquidAdapted adapted)) throw new InvalidITempatedItemException(item, this);
            return $"User/user-{adapted.User.Handle}-{adapted.Scheme.Handle}";
        }
    }
}
