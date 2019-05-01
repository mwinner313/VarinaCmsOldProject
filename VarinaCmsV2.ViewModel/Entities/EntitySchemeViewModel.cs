using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.ViewModel.Entities
{
    public class EntitySchemeViewModel:BaseVeiwModel
    {
        public List<FieldDefenitionViewModel> FieldDefenitions { get; set; }
        public List<FieldDefenitionGroupViewModel> FieldDefenitionGroups { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string Handle { get; set; }
        [Required]
        public string Url { get; set; }

        public SchemeType Type { get; set; }

    }
}
