using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.ViewModel.Entities
{
    public class EntitySchemeUpdateViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string Handle { get; set; }
    }
}
