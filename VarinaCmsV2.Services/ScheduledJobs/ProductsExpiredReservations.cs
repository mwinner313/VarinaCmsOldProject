using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Hosting;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;

namespace VarinaCmsV2.Services.ScheduledJobs
{
    public class ProductsExpiredReservations : InjectibleJob, IRegisteredObject
    {
        private readonly IOrderDataService _orderDataService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly object _lock = new object();
        private bool _shuttingDown;
        public ProductsExpiredReservations()
        {
            _unitOfWork = Container.GetInstance<IUnitOfWork>();
            _orderDataService = Container.GetInstance<IOrderDataService>();
            HostingEnvironment.RegisterObject(this);
        }

        public override void Execute()
        {

            if (_shuttingDown)
                return;
            var oneHourAge = DateTime.Now.AddHours(-1);
            var items = _orderDataService.Query
                   .Include(x => x.OrderItems)
                   .Include(x => x.OrderItems.Select(c => c.Product))
                   .Where(x => x.CreateDateTime < oneHourAge && x.PaymentStatus == PaymentStatus.Pending && x.OrderStatus != OrderStatus.Cancelled).ToList();

            items.ForEach(order =>
            {
                order.OrderStatus = OrderStatus.Cancelled;
                foreach (var orderItem in order.OrderItems)
                {
                    if (orderItem.Product.Inventory.TrackMethod == InventoryTrackMethod.TrackByAvalibleQuantity)
                    {
                        if (orderItem.Quantity <= orderItem.Product.Inventory.ReservedQuantity)
                        {
                            orderItem.Product.Inventory.ReservedQuantity -= orderItem.Quantity;
                            _unitOfWork.Update(orderItem.Product);
                        }
                    }
                }
                _unitOfWork.Update(order);
            });
            _unitOfWork.SaveChanges();
        }

        public void Stop(bool immediate)
        {
            // Locking here will wait for the lock in Execute to be released until this code can continue.
            lock (_lock)
            {
                _shuttingDown = true;
            }
            HostingEnvironment.UnregisterObject(this);
        }
    }
}