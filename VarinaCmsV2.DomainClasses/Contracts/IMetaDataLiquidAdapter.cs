using DotLiquid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.DomainClasses.Contracts
{
    public interface IMetaDataLiquidAdapter
    {
        object GetLiquidAdaptedData(JObject data);
        bool IsValidFor(string metaType);
    }
}
