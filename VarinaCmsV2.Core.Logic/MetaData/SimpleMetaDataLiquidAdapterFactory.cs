using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Logic.MetaData
{

    public class SimpleMetaDataLiquidAdapterFactory : IMetaDataLiquidAdapterFactory
    {
        private readonly List<IMetaDataLiquidAdapter> _adapters;
        public SimpleMetaDataLiquidAdapterFactory(List<IMetaDataLiquidAdapter> adapters)
        {
            _adapters = adapters;
        }
        public IMetaDataLiquidAdapter Find(string typeName)
        {
            return _adapters.First(x => x.IsValidFor(typeName));
        }
    }
}
