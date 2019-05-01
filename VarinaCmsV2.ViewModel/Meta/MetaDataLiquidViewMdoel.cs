using DotLiquid;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.ViewModel.Meta
{
   public  class MetaDataLiquidViewMdoel:ILiquidizable
    {
        private readonly IMetaDataLiquidAdapterFactory _adapterFactory;
        public MetaDataLiquidViewMdoel(IMetaDataLiquidAdapterFactory adapterFactory)
        {
            _adapterFactory = adapterFactory;
        }
        public JObject MetaData { get; set; }
        public Guid Id { get; set; }
        public string MetaName { get; set; }
        public object ToLiquid()
        {
            return _adapterFactory.Find(MetaName).GetLiquidAdaptedData(MetaData);
        }
    }
}
