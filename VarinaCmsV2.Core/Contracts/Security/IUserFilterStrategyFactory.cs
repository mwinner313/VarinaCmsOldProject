using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Contracts.Security
{
    public interface IUserFilterStrategyFactory
    {
        IUserFilterStrategy GetStrategy(IIdentity identity);
    }
}
