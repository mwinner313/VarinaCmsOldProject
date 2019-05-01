using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Security
{
    public class AppRoleManager:RoleManager<Role,Guid>, IAppRoleManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppRoleManager(IRoleStore<Role, Guid> store, IUnitOfWork unitOfWork) : base(store)
        {
            _unitOfWork = unitOfWork;
        }

        public Role FindByName(string roleName)
        {
            return _unitOfWork.Set<Role>().FirstOrDefault(x => x.Name == roleName);
        }
    }
}
