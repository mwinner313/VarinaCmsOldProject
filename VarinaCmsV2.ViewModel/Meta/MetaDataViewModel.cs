using System;
using Newtonsoft.Json.Linq;

namespace VarinaCmsV2.ViewModel.Meta
{
    public class MetaDataViewModel:BaseVeiwModel
    {
        public string MetaName { get; set; }
        public Guid TargetId { get; set; }
        public JObject MetaData { get; set; }
    }
}