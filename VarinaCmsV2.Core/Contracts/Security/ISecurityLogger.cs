using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;

namespace VarinaCmsV2.Core.Contracts.Security
{
    public interface ISecurityLogger
    {
        void LogDangeriousUpdateAttemp(IPrincipal principal, object updateTarget);
        void LogDangeriousDeleteAttemp(IPrincipal principal, object deleteTarget);
        void LogDangeriousAddAttemp(IPrincipal principal, object data);
        void LogDangeriousReadAttemp(IPrincipal principal, object data);

        void LogDangeriousActionAttemp(IPrincipal principal, object data,string action);
    }
}
