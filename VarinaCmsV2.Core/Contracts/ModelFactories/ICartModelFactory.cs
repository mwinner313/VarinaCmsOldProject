using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.Core.Contracts.ModelFactories
{
    public interface ICartModelFactory
    {
        LiquidAdapter GetCurrentUserCartModel();
    }
}
