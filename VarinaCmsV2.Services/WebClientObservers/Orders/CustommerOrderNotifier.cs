using System.Threading.Tasks;
using FluentScheduler;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.WebClientServices.Orders;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.Services.ScheduledJobs;

namespace VarinaCmsV2.Services.WebClientObservers.Orders
{
    public class CustommerOrderNotifier : IOrderWebClientObserver
    {
        private readonly IEmailyService _emailService;

        public CustommerOrderNotifier(IEmailyService emailService)
        {
            _emailService = emailService;
        }

        public async Task OrderPlaced(Order order)
        {

        }

        public async Task OrderPaid(Order order)
        {
            JobManager.AddJob(new UserMessageNotifier(order.Creator.Email, "ثبت سفارش",
                    $"سفارش شما با موفقیت ثبت شد شماره پیگیری سفارش : {order.OrderTrackNumber} .")
                , (schedule) => schedule.ToRunNow());
        }
    }
}
