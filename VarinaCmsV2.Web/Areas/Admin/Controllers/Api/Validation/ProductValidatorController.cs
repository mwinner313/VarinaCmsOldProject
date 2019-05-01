using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.Web.Areas.Admin.Models;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api.Validation
{
    [WebApiCmsAuthorize(Premissions = ProductPremission.CanSee)]

    public class ProductValidatorController : ApiController
    {
        private readonly IProductDataService _productDataService;

        public ProductValidatorController(IProductDataService productDataService)
        {
            _productDataService = productDataService;
        }


        public async Task<IHttpActionResult> ExistsTitle(ProductValidationContext model)
        {
            if (model.Id.HasValue && await _productDataService.Query.AnyAsync(x =>
                    x.Title == model.Value && x.Id == model.Id))
                return Ok(new
                {
                    value = model,
                    isValid = true
                });

            return Ok(new
            {
                value = model,
                isValid = !await _productDataService.Query.AnyAsync(x => x.Title == model.Value)
            });
        }
    }
}
