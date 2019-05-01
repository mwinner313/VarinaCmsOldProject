using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Contracts.ModelFactories
{
    public interface ICheckoutModelFactory
    {
        LiquidAdapter PrepareShippingPageViewData();
        LiquidAdapter PreparePaymentPageViewData();
    }
}
