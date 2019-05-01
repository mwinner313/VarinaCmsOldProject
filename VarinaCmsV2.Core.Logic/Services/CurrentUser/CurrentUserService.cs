using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.WebClientServices.Discounts;
using VarinaCmsV2.Core.CustomAttributes;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Services.CurrentUser;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.FileManager.Files;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.ViewModel.User;
using VarinaCmsV2.ViewModel.User.Account;

namespace VarinaCmsV2.Core.Logic.Services.CurrentUser
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly Lazy<Guid> _currUserId;
        private readonly IDiscountDataService _discountDataService;
        private readonly IDiscountValidator _discountValidator;
        private readonly IFileManager _fileManager;
        private readonly IIdentityManager _identityManager;
        private readonly IShoppingCartItemDataService _shoppingCartItemDataService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<UserAttribute> _userAttributes;
        private readonly ICouponCodeDataService _couponCodeDataService;
        private readonly IOrderDataService _orderDataService;
        private readonly IAppUserManager _userManager;
        private readonly IWorkContext _workContext;
        private readonly IAddressDataService _addressDataService;
        private List<CouponCodeUsage> _cachedCouponCodeUsages;

        public CurrentUserService(IIdentityManager identityManager, IAppUserManager userManager, IUnitOfWork unitOfWork,
            IFileManager fileManager, IDiscountDataService discountDataService, IWorkContext workContext,
            IShoppingCartItemDataService shoppingCartItemDataService, IDiscountValidator discountValidator, ICouponCodeDataService couponCodeDataService, IOrderDataService orderDataService, IAddressDataService addressDataService)
        {
            _identityManager = identityManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _discountDataService = discountDataService;
            _workContext = workContext;
            _shoppingCartItemDataService = shoppingCartItemDataService;
            _discountValidator = discountValidator;
            _couponCodeDataService = couponCodeDataService;
            _orderDataService = orderDataService;
            _addressDataService = addressDataService;
            _userAttributes = unitOfWork.Set<UserAttribute>();
            _currUserId = new Lazy<Guid>(() => _userManager.GetCurrentUserId());
        }

        public async Task Edit(EditCurrentUserModel request)
        {
            if (request.Action == UserEditAction.ChangePassword)
                await ChangePassword(request.ChangePasswordViewModel);

            if (request.Action == UserEditAction.EditInfo)
                await ChangeInfo(request.UserInfoEditViewModel);

            if (request.Action == UserEditAction.ChangeAvatar)
                await ChangeAvatar(request.UserAvatar);
        }

        public async Task SaveCustomAttributeAsync(string name, string value)
        {
            var currUser = _userManager.GetCurrentUser();
            var attr = currUser.Attributes.FirstOrDefault(x => x.Name == name);
            if (attr != null)
            {
                attr.Value = value;
                _unitOfWork.Update(attr);
            }
            else
            {
                currUser.Attributes.Add(new UserAttribute { Name = name, Value = value });
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public Task AddNewAddressAsync(AddressAddOrUpdateModel vm)
        {
            var model = vm.MapToModel();
            model.Id = Guid.Empty;
            var currUser = _userManager.GetCurrentUser();
            currUser.Addresses.Add(model);
            return _unitOfWork.SaveChangesAsync();
        }

        public Task DeleteAddress(Guid id)
        {
            var address = _unitOfWork.Set<Address>().FirstOrDefault(x => x.Id == id && x.UserId == _currUserId.Value);

            if (address != null)
            {
                _unitOfWork.Delete(address);
                return _unitOfWork.SaveChangesAsync();
            }
            return Task.FromResult(0);
        }

        public Task UpdateAddress(AddressAddOrUpdateModel vm)
        {
            var exitingAddress = _unitOfWork.Set<Address>()
                .FirstOrDefault(x => x.Id == vm.Id && x.UserId == _currUserId.Value);
            if (exitingAddress is null) return Task.FromResult(0);

            vm.MapToExiting(exitingAddress);
            _unitOfWork.Update(exitingAddress);
            return _unitOfWork.SaveChangesAsync();
        }

        public UserAttribute GetCustomAttribute(string name)
        {
            return _workContext.CurrentUser.Attributes.FirstOrDefault(x => x.Name == name);
        }

        public async Task DeleteCustomAttributeAsync(string name)
        {
            var attr = _userAttributes.FirstOrDefault(x => x.Name == name && x.UserId == _currUserId.Value);
            if (attr is null) return;
            _unitOfWork.Delete(attr);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<CouponUsageResponse> AddCouponCodeUsage(string coupon)
        {
            var res = new CouponUsageResponse { Succeed = false, Message = "کد تخفیف وارد شده معتبر نمی باشد." };

            var currUserId = _identityManager.GetCurrentIdentity().GetUserId().ToGuid();

            var discount = await
                _discountDataService.Query.FirstOrDefaultAsync(x =>
                    x.CouponCode == coupon || x.CouponCodes.Any(cc => cc.Code == coupon));

            if (discount == null) return res;
            if (_couponCodeDataService.Query.Any(x => x.Code == coupon && (x.Reserved || x.UsedById.HasValue)))
            {
                res.Message = "این کد تخفیف منقضی شده";
            }

            if (discount.CouponCodeType == CouponCodeType.Single && !_discountValidator.CanUseBaseOnLimitation(discount, currUserId, res)) return res;
            if (!_discountValidator.CanUseInCartBaseOnProductCategoryAndSku(discount, res)) return res;
            if (!_discountValidator.CanUseInCartBaseOnOrderTotal(discount, res)) return res;

            var exiting = GetCustomAttribute(UserAttributes.CouponCodeUsage + coupon);
            if (exiting == null)
                await SaveCustomAttributeAsync(UserAttributes.CouponCodeUsage + coupon, coupon);

            res.Succeed = true;
            res.Message = "کپن تخفیف اعمال شد";
            _cachedCouponCodeUsages?.Add(new CouponCodeUsage() { Code = coupon, DisocuntId = discount.Id, DiscountName = discount.Name });
            return res;
        }

        public List<CouponCodeUsage> GetUsedCouponCodeUsages()
        {
            if (_cachedCouponCodeUsages != null) return _cachedCouponCodeUsages;

            var usedCouponCodes = GetUserCopuponUsagesFromAttributes();
            var couponCodeUsages = new List<CouponCodeUsage>();
            GetUsedCouponCodeDiscounts().Include(x => x.CouponCodes).ToList().ForEach(x =>
            {
                if (x.CouponCodeType == CouponCodeType.Multi)
                {
                    var couponCode = x.CouponCodes.First(c => usedCouponCodes.Contains(x.CouponCode));
                    var couponUsageResponse = new CouponUsageResponse();
                    couponCodeUsages.Add(new CouponCodeUsage
                    {
                        Code = couponCode.Code,
                        DisocuntId = x.Id,
                        DiscountName = x.Name,
                        Expired = couponCode.UsedById.HasValue || !_discountValidator.CanUseBaseOnStartAndEndDate(x)
                                  || !_discountValidator.CanUseInCartBaseOnOrderTotal(x, couponUsageResponse)
                                  || !_discountValidator.CanUseInCartBaseOnProductCategoryAndSku(x, couponUsageResponse),
                    });
                }
                else
                {
                    var couponUsageResponse = new CouponUsageResponse();
                    couponCodeUsages.Add(new CouponCodeUsage
                    {
                        Code = x.CouponCode,
                        DisocuntId = x.Id,
                        DiscountName = x.Name,
                        Expired =
                            !_discountValidator.CanUseBaseOnLimitation(x, _currUserId.Value, couponUsageResponse)
                            || !_discountValidator.CanUseBaseOnStartAndEndDate(x)
                            || !_discountValidator.CanUseInCartBaseOnOrderTotal(x, couponUsageResponse)
                            || !_discountValidator.CanUseInCartBaseOnProductCategoryAndSku(x, couponUsageResponse),
                        Message = couponUsageResponse.Message
                    });
                }
            });
            _cachedCouponCodeUsages = couponCodeUsages;
            return couponCodeUsages;
        }

        public Task ClearCouponCodeUsagesAsync()
        {
            _userAttributes.Where(x => x.UserId == _currUserId.Value).ToList().ForEach(_unitOfWork.Delete);
            return _unitOfWork.SaveChangesAsync();
        }

        public IQueryable<Discount> GetUsedCouponCodeDiscounts()
        {
            var usedCouponCodes = GetUserCopuponUsagesFromAttributes();

            return _discountDataService.Query
                .Where(x => usedCouponCodes.Contains(x.CouponCode) ||
                            x.CouponCodes.Any(c => usedCouponCodes.Contains(c.Code)));
        }

        public Task ClearCartAsync()
        {
            _shoppingCartItemDataService.Query.Where(x => x.CreatorId == _currUserId.Value).ToList()
                .ForEach(_unitOfWork.Delete);
            return _unitOfWork.SaveChangesAsync();
        }

        private List<string> GetUserCopuponUsagesFromAttributes()
        {
            var usedCouponCodes = new List<string>();

            foreach (var s in _workContext.CurrentUser.Attributes
                .Where(x => x.Name.StartsWith(UserAttributes.CouponCodeUsage))
                .Select(x => x.Name))
                usedCouponCodes.Add(s.Replace(UserAttributes.CouponCodeUsage, ""));
            return usedCouponCodes;
        }

        private async Task ChangeAvatar(HttpContent image)
        {
            var res = await _fileManager.SaveAsync(image,
                AppDomain.CurrentDomain.BaseDirectory + SettingHelper.UsersAvatarsPath);
            var currUser = await _userManager.GetCurrentUserAsync();
            currUser.AvatarPath = res.SavedFilePath;
            _unitOfWork.Update(currUser);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task ChangeInfo(UserInfoEditViewModel info)
        {
            var currUser = await _userManager.GetCurrentUserAsync();
            info.MapToExisting(currUser);
            _unitOfWork.Update(currUser);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task ChangePassword(ChangePasswordViewModel context)
        {
            var res = await _userManager.ChangePasswordAsync(_identityManager.GetCurrentIdentity().GetUserId().ToGuid(),
                context.CurrentPassword, context.NewPassword);
            if (!res.Succeeded)
                throw new InvalidOperationException(string.Join(",", res.Errors));
        }

        public Task<List<Order>> GetOrders(int pageNumber, int pageSize)
        {
            return _orderDataService.Query
                .Include(x => x.OrderItems)
                .Include(x => x.DiscountUsageHistories)
                .Include(x => x.OrderItems.Select(i => i.Product))
                .Paginate(pageSize, pageNumber)
                .Where(x => x.CreatorId == _currUserId.Value).ToListAsync();
        }


        public async Task<UserAddressesResponse> GetAddresses()
        {
            return new UserAddressesResponse()
            {
                Addresses = await _addressDataService.Query.Where(x => x.UserId == _currUserId.Value).ToListAsync(),
                SelectedAddressId =  GetCustomAttribute(UserAttributes.SelectedShippingAddressId)?.Value.ToGuid()
            };
        }
    }
}