using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Security.Contracts
{
    public interface IAppRoleManager
    {
        void Dispose();
        Task<IdentityResult> CreateAsync(Role role);
        Task<IdentityResult> UpdateAsync(Role role);
        Task<IdentityResult> DeleteAsync(Role role);
        Task<bool> RoleExistsAsync(string roleName);
        Task<Role> FindByIdAsync(Guid roleId);
        Task<Role> FindByNameAsync(string roleName);
        Role FindByName(string roleName);
        IIdentityValidator<Role> RoleValidator { get; set; }
        IQueryable<Role> Roles { get; }
    }
}