using System;
using System.Linq;
using System.Web.Hosting;
using StructureMap;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;

namespace VarinaCmsV2.Services.ScheduledJobs
{
    public class CouponCodeExpiredReservations:InjectibleJob, IRegisteredObject
    {
        private readonly IDiscountUsageDataService _discountUsageDataService;
        private readonly ICouponCodeDataService _couponCodeDataService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly object _lock = new object();
        private bool _shuttingDown;
        public CouponCodeExpiredReservations(IDiscountUsageDataService discountUsageDataService, ICouponCodeDataService couponCodeDataService)
        {
            _discountUsageDataService = discountUsageDataService;
            _couponCodeDataService = couponCodeDataService;
            _unitOfWork = Container.GetInstance<IUnitOfWork>();
            HostingEnvironment.RegisterObject(this);
        }

        public override void Execute()
        {
            if (_shuttingDown)
                return;
            var oneHourAge = DateTime.Now.AddHours(-1);
            var reusableCouponCodes = _discountUsageDataService.Query
           
                .Where(x => x.CreateDateTime < oneHourAge && x.Order.PaymentStatus == PaymentStatus.Pending && x.Order.OrderStatus != OrderStatus.Cancelled&&x.CouponCodeId.HasValue).Select(x=>x.CouponCodeId.Value).ToList();
            var items = _couponCodeDataService.Query.Where(x => reusableCouponCodes.Contains(x.Id)).ToList();
            items.ForEach(c =>
            {
                c.Reserved = false;
                _unitOfWork.Update(c);
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
