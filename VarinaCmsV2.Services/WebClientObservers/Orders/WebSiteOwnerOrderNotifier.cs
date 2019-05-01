using System;
using System.Threading.Tasks;
using System.Web;
using FluentScheduler;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.WebClientServices.Orders;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.Services.ScheduledJobs;

namespace VarinaCmsV2.Services.WebClientObservers.Orders
{
    public class WebSiteOwnerOrderNotifier : IOrderWebClientObserver
    {
        private readonly IEmailyService _emailService;
        private readonly IAppUserManager _userManager;


        public WebSiteOwnerOrderNotifier(IEmailyService emailService, IAppUserManager userManager)
        {

            _emailService = emailService;
            _userManager = userManager;
        }

        public async Task OrderPlaced(Order order)
        {
            _userManager.GetAdminstrators().ForEach(x =>
                JobManager.AddJob(new UserMessageNotifier(x.Email, "ثبت سفارش در وب سایت من", $"<a href='{ HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}/panel/fa-IR/eshop/order-details/{order.Id}'>برای مشاهده جزییات سفارش  کلیک کنید.!</a>")
                    , (schedule) => schedule.ToRunNow()));
          
        }

        public async Task OrderPaid(Order order)
        {
            _userManager.GetAdminstrators().ForEach(x => 
            JobManager.AddJob(new UserMessageNotifier(x.Email, "پرداخت هزینه سفارش در وب سایت من", $"<a href='{ HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}/panel/fa-IR/eshop/order-details/{order.Id}'>برای مشاهده جزییات سفارش  کلیک کنید.سفارش پرداخت شد!</a>")
                    , (schedule) => schedule.ToRunNow()));
        }
    }
}
