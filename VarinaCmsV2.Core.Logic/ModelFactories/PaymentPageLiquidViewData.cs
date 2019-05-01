using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Logic.ModelFactories
{
    internal class PaymentPageLiquidViewData : LiquidAdapter
    {
        public string PaymentId { get; set; }
        public string MerchantId { get; set; }
        public string ClientToken { get; set; }
    }
}