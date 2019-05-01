using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using Newtonsoft.Json.Linq;

namespace VarinaCmsV2.ViewModel.Meta
{
    public class MetaDataAddOrUpdateViewModel:BaseVeiwModel
    {
        [Required]
        public Guid TargetId { get; set; }
        [Required]
        public string MetaName { get; set; }
        [Required]
        public JObject MetaData { get; set; }
        public bool HasChanges { get; set; }
    }
}
