using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Contracts.Security
{
    public interface IRestrictedItemAccessManager
    {
        bool HasAccess(IAccessRestrictedItem restricted,  AccessPremission premission);
        IQueryable<T> Filter<T>(IQueryable<T> items) where T : class, IAccessRestrictedItem;
    }
}
