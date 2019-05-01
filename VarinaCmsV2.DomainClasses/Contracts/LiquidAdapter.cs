using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid;

namespace VarinaCmsV2.DomainClasses.Contracts
{
    public class LiquidAdapter: Drop, ITemplatedItem
    {
        public string Handle { get; set; }
    }
}
