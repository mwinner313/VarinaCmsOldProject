using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.WebClientServices.Discounts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.DomainClasses.Entities.EShop.Shipping;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.Core.Services.CurrentUser
{
    public interface ICurrentUserService
    {
        Task Edit(EditCurrentUserModel request);
        Task AddNewAddressAsync(AddressAddOrUpdateModel vm);
        Task DeleteAddress (Guid id);
        Task UpdateAddress (AddressAddOrUpdateModel vm);
        Task SaveCustomAttributeAsync(string name, string value);
        UserAttribute GetCustomAttribute(string name);
        Task DeleteCustomAttributeAsync(string name);
        Task ClearCartAsync();
        List<CouponCodeUsage> GetUsedCouponCodeUsages();
        Task ClearCouponCodeUsagesAsync();
        IQueryable<Discount> GetUsedCouponCodeDiscounts();
        Task<CouponUsageResponse> AddCouponCodeUsage(string coupon);
        Task<List<Order>> GetOrders(int pageNumber, int pageSize);
        Task<UserAddressesResponse> GetAddresses();

    }
}
