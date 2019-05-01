using System;
using Newtonsoft.Json.Linq;

namespace VarinaCmsV2.ViewModel.Entities
{
    public class FieldViewModel:BaseVeiwModel
    {
        public Guid? EntityId { get; set; }
        public Guid FieldDefenitionId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid FieldDefenitionFieldDefenitionGroupId { get; set; }
        public JObject RawValue { get; set; }
        public FieldDefenitionViewModel Fd { get; set; }
    }
}