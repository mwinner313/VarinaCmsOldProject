using DotLiquid;

namespace VarinaCmsV2.DomainClasses.Contracts
{
    public abstract class UrlableLiquidAdapter: LiquidAdapter,IUrlableViewModel
    {
        public string ToUrl { get; set; }
        public string ToFullUrl { get; set; }
    }
}