using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Contracts.WebClientServices.Orders;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Services.Orders;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Web.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Custom-Header")]
    public class PaymentController : LiquidController
    {
        private readonly IOrderWebClientService _orderService;
        private readonly ISecurityLogger _securityLogger;
        public PaymentController(IOrderWebClientService orderService, ISecurityLogger securityLogger)
        {
            _securityLogger = securityLogger;
            _orderService = orderService;
        }
        public async Task<ActionResult> Index(string token, string merchantId, string resultCode, string paymentId, string referenceId)
        {
            var sha1Key = "22338240992352910814917221751200141041845518824222260";
            using (var verifyService = new Ipg.Verify.VerifyClient())
            {

                if (!string.IsNullOrEmpty(resultCode) && resultCode == "100")
                {
                    var res = verifyService.KicccPaymentsVerification(token, merchantId, referenceId, sha1Key);
                    if (res > 0)
                    {
                        var order = await _orderService.PostProcessOrderPaymentAsync(paymentId.ToGuid());

                        return LiquidView(order.MapToLiquidViewModel(),"paymentsucceed");
                    }
                    else
                    {
                            _securityLogger.LogDangeriousActionAttemp(User, new
                            {
                                token,
                                merchantId,
                                paymentId,
                                referenceId
                            }, $"InvalidPostPaymentProcess in {Request?.Url?.PathAndQuery}");
                    }
                }

            }
            return LiquidView<LiquidAdapter>(null, "paymentsucceed");
        }
    }
}