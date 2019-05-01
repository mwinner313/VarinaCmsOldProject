using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Discounts;
using VarinaCmsV2.Core.Services.Entity;
using VarinaCmsV2.Core.Web.ActionFilters;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.Eshop.Discount;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    public class DiscountController : BaseApiController
    {
        private readonly IDiscountService _discountService;

        public DiscountController( IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [WebApiCmsAuthorize(Premissions = DiscountPremission.CanSee)]
        public async Task<IHttpActionResult> Get([FromUri]DiscountQuery query)
        {
            IHttpActionResult res = BadRequest();
            var serviceRes = await _discountService.Get(new DiscountListRequest()
            {
                Query = query ?? new DiscountQuery(),
                RequestOwner = this.User
            });

            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes);

            return res;
        }
        [WebApiCmsAuthorize(Premissions = DiscountPremission.CanSee)]

        public async Task<IHttpActionResult> Get( Guid id)
        {
            IHttpActionResult res = BadRequest();

            var serviceRes = await _discountService.Get(new DiscountRequest()
            {
                Id = id,
                RequestOwner = User
            });

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.Model);

            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }

        [System.Web.Mvc.HttpPost]
        [WebApiCmsAuthorize(Premissions = DiscountPremission.CanAdd)]
        [WebApiEnableEntityValidation]
        public async Task<IHttpActionResult> Post(DiscountAddOrUpdateModel model)
        {
            IHttpActionResult res = BadRequest();
            if (!ModelState.IsValid) return res;

            var serviceRes = await _discountService.Add(new DiscountAddRequest()
            {
                RequestOwner = User,
                ViewModel = model
            });

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(model);

            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
        [System.Web.Mvc.HttpPut]
        [WebApiCmsAuthorize(Premissions = EntityPremission.CanEdit)]
        [WebApiEnableEntityValidation]
        public async Task<IHttpActionResult> Put(Guid id, DiscountAddOrUpdateModel model)
        {
            IHttpActionResult res = BadRequest();
            if (!ModelState.IsValid)
                return
                    base.BadRequest(GetModelStateErorrs());

            if (id != model.Id) return res;

            var serviceRes = await _discountService.Edit(new DisocuntEditRequest()
            {
                RequestOwner = User,
                Model = model,
                DiscountId = id,
            });

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(model);

            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }



        [HttpDelete]
        [WebApiCmsAuthorize(Premissions = EntityPremission.CanDelete)]
        [WebApiEnableEntityValidation]
        public async Task<IHttpActionResult> Delete( Guid id)
        {
            IHttpActionResult res = BadRequest();
            var serviceRes = await _discountService.Delete(new DisocuntDeleteRequest()
            {
                RequestOwner = User,
               Id = id
            });

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok();

            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);
            if (serviceRes.Access == ResponseAccess.BadRequest)
                return BadRequest(serviceRes.Message);
            return res;
        }
    }
}