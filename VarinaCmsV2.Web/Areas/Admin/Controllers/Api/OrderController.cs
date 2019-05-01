using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Orders;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.ViewModel.Eshop;
using VarinaCmsV2.ViewModel.Eshop.Orders;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    [WebApiCmsAuthorize]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [WebApiCmsAuthorize(Premissions = OrderPremission.CanSee)]
        public async Task<IHttpActionResult> Get([FromUri] OrderQuery query)
        {
            var serviceRes = await _orderService.Get(new OrderListRequest()
            {
                RequestOwner = User,
                Query = query ?? new OrderQuery()
            });

            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes);
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }

        [HttpGet]
        [WebApiCmsAuthorize(Premissions = OrderPremission.CanSee)]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var serviceRes = await _orderService.Get(new OrderGetRequest()
            {
                RequestOwner = User,
                OrderId = id
            });

            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.Order);
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }

        [HttpPut]
        [WebApiCmsAuthorize(Premissions = OrderPremission.CanEdit)]
        public async Task<IHttpActionResult> Put(Guid id, OrderAddOrUpdateViewModel model)
        {
            var serviceRes = await _orderService.Edit(new OrderEditRequest()
            {
                RequestOwner = User,
                Model = model,
                Id = id
            });

            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes);
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }

        [HttpPatch]
        [Route("api/order/ChangeOrderItemDownLoadActivationState/{itemId}")]
        [WebApiCmsAuthorize(Premissions = OrderPremission.CanEdit)]
        public async Task<IHttpActionResult> Patch(Guid itemId)
        {
            await _orderService.ChangeOrderItemDownLoadActivationState(itemId);
            return Ok();
        }

        [HttpPatch]
        [Route("api/order/SendOrderShippmentStatusChangedNotification/{orderId}")]
        [WebApiCmsAuthorize(Premissions = OrderPremission.CanEdit)]
        public async Task<IHttpActionResult> SendOrderShippmentStatusChangedNotification(Guid orderId)
        {
            await _orderService.SendOrderShippmentStatusChangedNotification(orderId);
            return Ok();
        }

        [HttpPatch]
        [Route("api/order/ChangeSeenStateByAdmin/{orderId}")]
        [WebApiCmsAuthorize(Premissions = OrderPremission.CanEdit)]
        public async Task<IHttpActionResult> ChangeSeenStateByAdmin(Guid orderId)
        {
            await _orderService.ChangeSeenStateByAdmin(orderId);
            return Ok();
        }
    }
}