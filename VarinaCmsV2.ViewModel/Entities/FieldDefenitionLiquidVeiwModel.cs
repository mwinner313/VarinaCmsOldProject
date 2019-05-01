using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid;
using Newtonsoft.Json.Linq;

namespace VarinaCmsV2.ViewModel.Entities
{
    public class FieldDefenitionLiquidVeiwModel:Drop
    {
        public Guid Id { get; set; }    
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool IsArray { get; set; }
        public string Handle { get; set; }
        public bool IsRequired { get; set; }
        public Guid? FieldDefenitionGroupId { get; set; }
        public int Order { get; set; }
        public JObject MetaData { get; set; }
        public JObject DefaultValue { get; set; }
    }
}
