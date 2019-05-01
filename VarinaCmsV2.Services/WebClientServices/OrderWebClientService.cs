using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FluentScheduler;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.WebClientServices.Discounts;
using VarinaCmsV2.Core.Contracts.WebClientServices.Orders;
using VarinaCmsV2.Core.CustomAttributes;
using VarinaCmsV2.Core.Services.CurrentUser;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.DomainClasses.Entities.EShop.Shipping;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.Services.ScheduledJobs;

namespace VarinaCmsV2.Services.WebClientServices
{
    public class OrderWebClientService : IOrderWebClientService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDiscountWebClientService _discountWebClientService;
        private readonly IOrderDataService _orderDataService;
        private readonly IOrderItemDataService _orderItemDataService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserManager _userManager;
        private readonly IWorkContext _workContext;

        public OrderWebClientService(IWorkContext workContext, IDiscountWebClientService discountWebClientService,
            ICurrentUserService currentUserService, IOrderDataService orderDataService, IUnitOfWork unitOfWork,
            IEmailyService emailyService, IAppUserManager userManager, IOrderItemDataService orderItemDataService)
        {
            _workContext = workContext;
            _discountWebClientService = discountWebClientService;
            _currentUserService = currentUserService;
            _orderDataService = orderDataService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _orderItemDataService = orderItemDataService;
        }

        public async Task<Order> PlaceCurrentUserCartAsync()
        {
            var newOrder = PrepareNewOrder();
            AddCartItemsToOrder(newOrder);
            CalculatePrices(newOrder);
            await _orderDataService.AddAsync(newOrder);
            await _currentUserService.SaveCustomAttributeAsync(UserAttributes.PaymentProccessingOrderId,
                newOrder.Id.ToString());
            await _currentUserService.ClearCartAsync();
            await _currentUserService.ClearCouponCodeUsagesAsync();
            return newOrder;
        }

        
        public async Task CountUpDownloadCountForProductOrderItem(Guid orderId, Guid productId)
        {
            var orderItem = _orderItemDataService.Query.Include(x => x.Product).First(
                x => x.OrderId == orderId && x.ProductId == productId);

            if (!orderItem.Product.UnlimitedDownloads)
            {
                orderItem.DownloadCount++;
                await _orderItemDataService.UpdateAsync(orderItem);
            }
        }

        public async Task<Order> PostProcessOrderPaymentAsync(Guid orderId)
        {
            var order = GetOrderWithPostPaymentProccesNessesaryDatas(orderId);
            if (order is null) return null;

            ActivateDownloadFiles(order);
            CountDownProductInventories(order);
            ApplyProductsMinStockQuantityAction(order);
            NotifyAdminForQuantityBelow(order.OrderItems.Select(x => x.Product).ToList());
            HandleStatusesAfterPayment(order);
            _discountWebClientService.ExpireUsedMultiCouponCodeDiscountAfterPaymentCompeleted(order);
            await _unitOfWork.SaveChangesAsync();

            return order;
        }

        private Order GetOrderWithPostPaymentProccesNessesaryDatas(Guid orderId)
        {
            return _orderDataService.Query
                .Include(x => x.OrderItems)
                .Include(x => x.Creator)
                .Include(x => x.DiscountUsageHistories)
                .Include(x => x.DiscountUsageHistories.Select(d => d.Discount))
                .Include(x => x.DiscountUsageHistories.Select(d => d.Discount.CouponCodes))
                .Include(x => x.OrderItems.Select(o => o.Product)).FirstOrDefault(x => x.Id == orderId);
        }

        private void CalculatePrices(Order newOrder)
        {
            newOrder.OrderSubTotal = newOrder.OrderItems.Sum(x => x.Price);
            _discountWebClientService.ApplyNeccesaryDiscountsOnOrder(newOrder);
            newOrder.OrderDiscount += newOrder.OrderItems.Sum(x => x.DiscountAmount);
            newOrder.OrderTotal = newOrder.OrderItems.Sum(x => x.Price) - newOrder.OrderDiscount;
        }

        private void AddCartItemsToOrder(Order newOrder)
        {
            _workContext.CurrentUser.ShoppingCartItems.ToList().ForEach(cartItem =>
            {
                newOrder.OrderItems.Add(new OrderItem
                {
                    Quantity = cartItem.Quantity,
                    ProductId = cartItem.ProductId,
                    Product = cartItem.Product,
                    UnitPrice = cartItem.Product.Price,
                    Price = cartItem.Quantity * cartItem.Product.Price +
                            (cartItem.Product.Shipment.IsShipEnabled ? cartItem.Product.Shipment.SendPrice : 0)
                });
                HandleReservation(cartItem);
            });
        }

        private void HandleReservation(ShoppingCartItem cartItem)
        {
            if (cartItem.Product.Inventory.TrackMethod == InventoryTrackMethod.TrackByAvalibleQuantity)
            {
                cartItem.Product.Inventory.ReservedQuantity += cartItem.Quantity;
                _unitOfWork.Update(cartItem.Product);
            }
        }

        private Order PrepareNewOrder()
        {
            return new Order
            {
                Creator = _workContext.CurrentUser,
                CreateDateTime = DateTime.Now,
                PaymentStatus = PaymentStatus.Pending,
                OrderStatus = OrderStatus.Pending,
                UpdateDateTime = DateTime.Now,
                PaymentMethod = _currentUserService.GetCustomAttribute(UserAttributes.SelectedPaymentMethod)?.Value,
                ShippingAddressId = _currentUserService.GetCustomAttribute(UserAttributes.SelectedShippingAddressId)
                    ?.Value.ToGuid(),
                ShippingMethod = _currentUserService.GetCustomAttribute(UserAttributes.SelectedShippingMethodId)?.Value,
                ShippingStatus = _workContext.CurrentUser.ShoppingCartItems.Any(x => x.Product.Shipment.IsShipEnabled)
                    ? ShippingStatus.NotYetShipped
                    : ShippingStatus.ShippingNotRequired
            };
        }
        private static void HandleStatusesAfterPayment(Order order)
        {
            order.PaymentStatus = PaymentStatus.Paid;
            order.OrderStatus = order.ShippingStatus == ShippingStatus.ShippingNotRequired
                ? OrderStatus.Complete
                : OrderStatus.Pending;
        }
        private void NotifyAdminForQuantityBelow(List<Product> products)
        {
            foreach (var adminstrator in _userManager.GetAdminstrators())
            foreach (var product in products.Where(x =>
                x.Inventory.TrackMethod == InventoryTrackMethod.TrackByAvalibleQuantity))
                if (product.Inventory.NotifyAdminForQuantityBelow <= product.Inventory.StockQuantity)
                    JobManager.AddJob(new ProductStockQuantityNotifier(product, adminstrator.Email),
                        s => s.ToRunOnceAt(DateTime.Now.AddMinutes(1)));
        }
        private void ApplyProductsMinStockQuantityAction(Order order)
        {
            foreach (var orderItem in order.OrderItems)
                if (orderItem.Product.Inventory.MinStockQuantity >= orderItem.Product.Inventory.StockQuantity)
                    switch (orderItem.Product.Inventory.MinStockQuntityAction)
                    {
                        case MinStockQuntityAction.DisableAddCardAdditon:
                            orderItem.Product.CanAddToCart = false;
                            _unitOfWork.Update(orderItem.Product);
                            break;
                        case MinStockQuntityAction.UnPublishProduct:
                            orderItem.Product.IsVisible = false;
                            _unitOfWork.Update(orderItem.Product);
                            break;
                    }
        }

        private void CountDownProductInventories(Order order)
        {
            foreach (var item in order.OrderItems)
                if (item.Product.Inventory.TrackMethod == InventoryTrackMethod.TrackByAvalibleQuantity)
                {
                    item.Product.Inventory.ReservedQuantity -= item.Quantity;
                    item.Product.Inventory.StockQuantity -= item.Quantity;
                }
        }

        private void ActivateDownloadFiles(Order order)
        {
            foreach (var item in order.OrderItems)
                if (item.Product.IsDownLoadFile &&
                    item.Product.DownLoadActivationType == ProductDownLoadActivationType.WhenPaid)
                {
                    item.IsDownloadActivated = true;
                    _unitOfWork.Update(item);
                }
        }
    }
}