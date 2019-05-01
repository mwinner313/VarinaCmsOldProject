using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using FluentScheduler;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Orders;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.DomainClasses.Entities.EShop.Shipping;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.Services.ScheduledJobs;

namespace VarinaCmsV2.Core.Logic.Services.Orders
{
    public class OrderService : BaseService<Order, OrderResponse, OrderListResponse>, IOrderService
    {
        private readonly IOrderDataService _orderDataService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdentityManager _identityManager;
        private readonly ISecurityLogger _securityLogger;
        private readonly IOrderLogger _orderLogger;
        private readonly IOrderLogDataService _orderLogDataService;
        public OrderService(List<IBaseBeforeAddingEntityLogic> baseBeforeAddingEntityLogics,
            List<BaseAfterAddingEntityLogic> baseAfterAddingEntityLogics,
            List<IBaseAfterUpdatingEntityLogic> baseAfterUpdateEntityLogics,
            List<IBaseBeforeUpdatingEntityLogic> baseBeforeUpdateEntityLogics,
            List<IBaseBeforeDeleteEntityLogic> baseBeforeDeleteEntityLogics,
            List<BaseAfterDeleteEntityLogic> baseAfterDeleteEntityLogics, IIdentityManager identityManager,
            IRestrictedItemAccessManager accessManager, IDataService<Order, Guid> dataSrv,
            IOrderDataService orderDataService, ISecurityLogger securityLogger, IUnitOfWork unitOfWork, IOrderLogger orderLogger, IOrderLogDataService orderLogDataService, IIdentityManager identityManager1) : base(baseBeforeAddingEntityLogics, baseAfterAddingEntityLogics,
            baseAfterUpdateEntityLogics, baseBeforeUpdateEntityLogics, baseBeforeDeleteEntityLogics,
            baseAfterDeleteEntityLogics, identityManager, accessManager, dataSrv)
        {
            _orderDataService = orderDataService;
            _securityLogger = securityLogger;
            _unitOfWork = unitOfWork;
            _orderLogger = orderLogger;
            _orderLogDataService = orderLogDataService;
            _identityManager = identityManager1;
        }

        public async Task<OrderListResponse> Get(OrderListRequest request)
        {
            var orders = _orderDataService.Query.FilterByQuery(request.Query).OrderByDescending(x => x.CreateDateTime)
                .Include(x => x.Creator);

            return new OrderListResponse
            {
                Access = ResponseAccess.Granted,
                Items = (await orders.Paginate(request.Query).AsNoTracking().ToListAsync()).MapToViewModel(),
                Count = await orders.LongCountAsync()
            };
        }

        public async Task<OrderResponse> Get(OrderGetRequest request)
        {
            var order = await _orderDataService.Query
                .AsNoTracking()
                .Include(x => x.Creator)
                .Include(x => x.OrderItems)
                .Include(x => x.DiscountUsageHistories)
                .Include(x => x.DiscountUsageHistories.Select(dh => dh.Discount))
                .Include(x => x.OrderItems.Select(i => i.Product))
                .Include(x => x.OrderItems.Select(i => i.Product.Pictures))
                .Include(x => x.ShippingAddress)
                .Include(x => x.ShippingAddress.StateProvince)
                .Include(x => x.OrderLogs)
                .Include(x => x.OrderLogs.Select(l => l.Creator))
                .Include(x => x.Shipment).FirstAsync(x => x.Id == request.OrderId);
            if (!HasAccessToSee(order, request.RequestOwner))
            {
                _securityLogger.LogDangeriousReadAttemp(request.RequestOwner, order);
                return UnauthorizedRequest("unauthorized access to this order !!!");
            }
            return new OrderResponse() { Access = ResponseAccess.Granted, Order = order.MapToViewModel() };
        }


        public async Task<OrderEditResponse> Edit(OrderEditRequest request)
        {
            var order = await _orderDataService.Query.Include(x => x.Shipment).FirstAsync(x => x.Id == request.Id);
            bool hasShipment = order.Shipment != null;
            if (!AccessManager.HasAccess(order, AccessPremission.See))
            {
                _securityLogger.LogDangeriousUpdateAttemp(request.RequestOwner, order);
                return new OrderEditResponse() { Access = ResponseAccess.Deny };
            }
            _orderLogger.OrderUpdated(order, request.Model.MapToModel());
            request.Model.MapToExisting(order);
            await BaseBeforeUpdateAsync(order, request.RequestOwner);
            if (order.ShippingStatus == ShippingStatus.Delivered && order.PaymentStatus == PaymentStatus.Paid)
                order.OrderStatus = OrderStatus.Complete;
            else
            {
                order.OrderStatus = OrderStatus.Pending;
            }

            _unitOfWork.Update(order);
            await BaseAfterUpdateAsync(order, request.RequestOwner);
            await _unitOfWork.SaveChangesAsync();
            return new OrderEditResponse()
            {
                Access = ResponseAccess.Granted,
                Logs = _orderLogDataService.Query.Include(x => x.Creator).Where(x => x.OrderId == order.Id).ToList().MapToViewModel()
            };
        }

        public async Task ChangeOrderItemDownLoadActivationState(Guid orderItemId)
        {


            var orderItem = _unitOfWork.Set<OrderItem>().Include(x => x.Order).First(x => x.Id == orderItemId);

            if (!AccessManager.HasAccess(orderItem.Order, AccessPremission.Manage))
            {
                _securityLogger.LogDangeriousActionAttemp(_identityManager.GetCurrentPrincipal(), orderItem, "change download file product");
            }

            orderItem.IsDownloadActivated = !orderItem.IsDownloadActivated;
            _unitOfWork.Update(orderItem);
            _orderLogger.OrderItemDownloadActivateStateChanged(orderItem);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<SimpleResonse> SendOrderShippmentStatusChangedNotification(Guid orderId)
        {
            var order = await _orderDataService.Query.Include(x => x.Shipment).Include(x => x.Creator).FirstOrDefaultAsync(x => x.Id == orderId);
            if (!AccessManager.HasAccess(order, AccessPremission.Manage))
            {
                _securityLogger.LogDangeriousActionAttemp(_identityManager.GetCurrentPrincipal(), order, "send notification to order owner for shipping status change");
                return new SimpleResonse() { Access = ResponseAccess.Deny };
            }
            if (order.ShippingStatus != ShippingStatus.Shipped)
            {
                return new SimpleResonse() { Access = ResponseAccess.BadRequest };
            }

            var shipmentTrackNumber = order.Shipment?.TrackingNumber;

            JobManager.AddJob(new UserMessageNotifier(order.Creator.Email, "وضعیت سفارش", "سفارش شما بسته بندی و ارسال شد " + shipmentTrackNumber), (s) => s.ToRunNow());

            return new SimpleResonse() { Access = ResponseAccess.Granted };
        }

        public async Task ChangeSeenStateByAdmin(Guid orderId)
        {
            if (_identityManager.CurrentIdentityHasOneOfRoles(new List<string>() { PreDefRoles.PrincipalAdministrator, PreDefRoles.Administrator }))
            {
                var order = await _orderDataService.Query.FirstAsync(x => x.Id == orderId);
                if (order.SeenByAdmin) return;
                order.SeenByAdmin = true;
                _unitOfWork.Update(order);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}