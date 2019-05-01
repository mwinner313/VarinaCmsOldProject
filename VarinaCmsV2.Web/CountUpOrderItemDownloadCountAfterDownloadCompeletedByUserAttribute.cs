using System;
using System.Web.Mvc;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.WebClientServices.Orders;
using VarinaCmsV2.Core.Contracts.WebClientServices.Products;

namespace VarinaCmsV2.Web.Controllers
{
    internal class CountUpOrderItemDownloadCountAfterDownloadCompeletedByUserAttribute : ActionFilterAttribute
    {
        private readonly IOrderWebClientService _orderWebClientService = Startup.Container.GetInstance<IOrderWebClientService>();
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is FileResult)
            {
                var productId = filterContext.RouteData.Values["productId"].ToString().ToGuid();
                var orderId = filterContext.RouteData.Values["orderId"].ToString().ToGuid();

                _orderWebClientService.CountUpDownloadCountForProductOrderItem(orderId, productId);
            }
        }
    }
}