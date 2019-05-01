using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop.Shipping;

namespace VarinaCmsV2.Core.Services.UserManagement
{
    public interface IUserManagementService
    {
        Task<UserResponse> GetAsync(UserGetRequest request);
        Task<UserListResponse> GetAsync(UserListRequest request);
        Task<UserResponse> AddAsync(UserAddRequest request);
        Task<UserResponse> UpdateAsync(UserUpdateRequest request);
        Task<UserResponse> DeleteAsync(UserDeleteRequest request);

    }
}
