using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Contracts.WebClientServices.Discounts;
using VarinaCmsV2.Core.CustomAttributes;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.LogicFacade;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.CurrentUser;
using VarinaCmsV2.Core.Services.Discounts;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.Services.WebClientServices;

namespace VarinaCmsV2.Core.Logic.Services.Discounts
{
    public class DiscountService : BaseService<Discount, DiscountResponse, DiscountListResponse>, IDiscountService
    {
        private readonly IDiscountDataService _discountDataService;
        private readonly IDiscountFacade _discountFacade;
        private readonly IDiscountUsageDataService _discountUsageDataService;
        private readonly ISecurityLogger _securityLogger;

  

        public async Task<DiscountListResponse> Get(DiscountListRequest request)
        {
            var discounts = AccessManager.Filter(_discountDataService.Query).FilterByQuery(request.Query);
            return new DiscountListResponse
            {
                Access = ResponseAccess.Granted,
                Items = (await discounts.OrderByDescending(x => x.Id).Paginate(request.Query).ToListAsync())
                    .MapToViewModel(),
                Count = await discounts.LongCountAsync()
            };
        }

        public async Task<DiscountResponse> Get(DiscountRequest request)
        {
            var discount = _discountDataService.Query
                .Include(x => x.AppliedToCategories)
                .Include(x => x.AppliedToProducts)
                .Include(x => x.AppliedToProducts.Select(p => p.Pictures)).FirstOrDefault(x => x.Id == request.Id);
            if (!HasAccessToSee(discount, request.RequestOwner))
            {
                _securityLogger.LogDangeriousReadAttemp(request.RequestOwner, discount);
                return new DiscountResponse { Access = ResponseAccess.Deny };
            }
            return new DiscountResponse { Access = ResponseAccess.Granted, Model = discount.MapToViewModel() };
        }

        public async Task<DiscountAddResponse> Add(DiscountAddRequest request)
        {
            var discout = request.ViewModel.MapToModel();
            await BaseBeforeAddAsync(discout, request.RequestOwner);
            try
            {
                await _discountFacade.AddAsync(discout);
            }
            catch (Exception e)
            {
             return new DiscountAddResponse(){Access = ResponseAccess.BadRequest,Message = e.Message};
            }
            await BaseAfterAddAsync(discout, request.RequestOwner);
            return new DiscountAddResponse { Access = ResponseAccess.Granted };
        }

        public async Task<DisountEditResponse> Edit(DisocuntEditRequest request)
        {
            var discount
                = _discountDataService.Query.Include(x => x.AppliedToCategories).Include(x => x.AppliedToProducts)
                    .FirstOrDefault(x => x.Id == request.DiscountId);
            await BaseBeforeUpdateAsync(discount, request.RequestOwner);
            try
            {
                await _discountFacade.UpadateAsync(discount, request.Model);

            }
            catch (Exception e)
            {
                return new DisountEditResponse() { Access = ResponseAccess.BadRequest, Message = e.Message };
            }
            await BaseAfterUpdateAsync(discount, request.RequestOwner);
            return new DisountEditResponse { Access = ResponseAccess.Granted };
        }

        public async Task<DisountDeleteResponse> Delete(DisocuntDeleteRequest request)
        {
            var res = new DisountDeleteResponse();
            var deletingDiscount = _discountDataService.Query.FirstOrDefault(x => x.Id == request.Id);

            if (deletingDiscount == null)
            {
                res.Message = "تخفیف مورد برای حذف یافت نشد !";
                res.Access = ResponseAccess.BadRequest;
                return res;
            }
            if (!HasAccessToManage(deletingDiscount, request.RequestOwner))
            {
                _securityLogger.LogDangeriousDeleteAttemp(request.RequestOwner, deletingDiscount);
                res.Access = ResponseAccess.Deny;
                return res;
            }

            if (_discountUsageDataService.Query.Any(x => x.DiscountId == request.Id))
            {
                res.Message = "تخفیف مورد نظر در چندین سفارش استفاده شده است حذف امکان پذیر نیست !";
                res.Access = ResponseAccess.BadRequest;
                return res;
            }

            await BaseBeforeDeleteAsync(deletingDiscount, request.RequestOwner);
            await _discountDataService.DeleteAsync(deletingDiscount.Id);
            await BaseAfterDeleteAsync(deletingDiscount, request.RequestOwner);
            res.Access = ResponseAccess.Granted;
            return res;
        }

        public DiscountService(List<IBaseBeforeAddingEntityLogic> baseBeforeAddingEntityLogics, List<BaseAfterAddingEntityLogic> baseAfterAddingEntityLogics, List<IBaseAfterUpdatingEntityLogic> baseAfterUpdateEntityLogics, List<IBaseBeforeUpdatingEntityLogic> baseBeforeUpdateEntityLogics, List<IBaseBeforeDeleteEntityLogic> baseBeforeDeleteEntityLogics, List<BaseAfterDeleteEntityLogic> baseAfterDeleteEntityLogics, IIdentityManager identityManager, IRestrictedItemAccessManager accessManager, IDataService<Discount, Guid> dataSrv, IDiscountDataService discountDataService, IDiscountFacade discountFacade, IDiscountUsageDataService discountUsageDataService, ISecurityLogger securityLogger) : base(baseBeforeAddingEntityLogics, baseAfterAddingEntityLogics, baseAfterUpdateEntityLogics, baseBeforeUpdateEntityLogics, baseBeforeDeleteEntityLogics, baseAfterDeleteEntityLogics, identityManager, accessManager, dataSrv)
        {
            _discountDataService = discountDataService;
            _discountFacade = discountFacade;
            _discountUsageDataService = discountUsageDataService;
            _securityLogger = securityLogger;
        }
    }
}