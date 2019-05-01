using System;
using Microsoft.Build.Framework;
using Newtonsoft.Json.Linq;

namespace VarinaCmsV2.ViewModel.Entities
{
    public class FieldAddOrUpdateViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid FieldDefenitionId { get; set; }
        [Required]
        public string FieldDefenitionType { get; set; }
        public Guid ?EntityId { get; set; }
        public Guid ?ProductId { get; set; }
        public string FieldDefenitionTitle { get; set; }
        public JObject RawValue { get; set; }
    }
}