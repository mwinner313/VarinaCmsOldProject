using System.Linq;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Core.Logic.Security.UserFilterStrategies
{
    internal class NoUserFilterStrategy : IUserFilterStrategy
    {
        public IQueryable<User> Filter(IQueryable<User> users)
        {
            return users;
        }
    }
}