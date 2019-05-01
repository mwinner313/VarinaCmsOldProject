using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.WebClientServices.Discounts;
using VarinaCmsV2.Core.CustomAttributes;
using VarinaCmsV2.Core.Services.CurrentUser;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.Services.Tests.McokServices
{
    public class MockCurrentUserService : ICurrentUserService
    {
        private readonly IWorkContext _workContext;
        private readonly List<Discount> _discounts = new List<Discount>();
        private readonly IIdentityManager _identityManager;
        private readonly IDiscountValidator _discountValidator;
        private readonly IDiscountDataService _discountDataService;
        public MockCurrentUserService(IWorkContext workContext, IIdentityManager identityManager, IDiscountValidator discountValidator, IDiscountDataService discountDataService)
        {
            _workContext = workContext;
            _identityManager = identityManager;
            _discountValidator = discountValidator;
            _discountDataService = discountDataService;
        }

        public Task Edit(EditCurrentUserModel request)
        {
            throw new NotImplementedException();
        }

        public async Task SaveCustomAttributeAsync(string name, string value)
        {
            var existing = _workContext.CurrentUser.Attributes.FirstOrDefault(x => x.Name == name);
            _workContext.CurrentUser.Attributes.Remove(existing);
            _workContext.CurrentUser.Attributes.Add(new UserAttribute() { Name = name, Value = value, Id = existing?.Id ?? Guid.NewGuid() });
        }

        public Task DeleteAddress(Guid id)
        {
            throw new NotImplementedException();
        }

        public UserAttribute GetCustomAttribute(string name)
        {
            return _workContext.CurrentUser.Attributes.FirstOrDefault(x => x.Name == name);
        }

        public Task DeleteCustomAttributeAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task ClearCartAsync()
        {
           _workContext.CurrentUser.ShoppingCartItems.Clear();
        }

        public List<CouponCodeUsage> GetUsedCouponCodeUsages()
        {
            throw new NotImplementedException();
        }

        public async Task ClearCouponCodeUsagesAsync()
        {
           
        }

        public IQueryable<Discount> GetUsedCouponCodeDiscounts()
        {
            return _discounts.AsQueryable();
        }

        public async Task<CouponUsageResponse> AddCouponCodeUsage(string coupon)
        {

            var res = new CouponUsageResponse { Succeed = false, Message = "کد تخفیف وارد شده معتبر نمی باشد." };

            var currUserId = _identityManager.GetCurrentIdentity().GetUserId().ToGuid();
            var discount = 
                _discountDataService.Query.FirstOrDefault(x =>
                    x.CouponCode == coupon || x.CouponCodes.Any(cc => cc.Code == coupon));

            if (discount == null) return res;

            if (!_discountValidator.CanUseBaseOnStartAndEndDate(discount)) return res;
            if (!_discountValidator.CanUseBaseOnLimitation(discount, currUserId, res)) return res;
            if (!_discountValidator.CanUseInCartBaseOnProductCategoryAndSku(discount, res)) return res;
            if (!_discountValidator.CanUseInCartBaseOnOrderTotal(discount, res)) return res;

            _discounts.Add(discount);

            res.Succeed = true;
            res.Message = "کپن تخفیف اعمال شد";
            return res;
        }

        public Task<List<Order>> GetOrders(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<UserAddressesResponse> GetAddresses()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAddress(AddressAddOrUpdateModel vm)
        {
            throw new NotImplementedException();
        }

        public Task AddNewAddressAsync(AddressAddOrUpdateModel vm)
        {
            throw new NotImplementedException();
        }
    }
}
