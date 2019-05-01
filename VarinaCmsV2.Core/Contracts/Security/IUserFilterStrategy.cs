using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Core.Contracts.Security
{
    public interface IUserFilterStrategy
    {
        IQueryable<User> Filter(IQueryable<User> users);
    }
}
