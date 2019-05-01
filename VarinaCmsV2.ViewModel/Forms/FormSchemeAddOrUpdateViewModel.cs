using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.ViewModel.Forms
{
    public class FormSchemeAddOrUpdateViewModel:BaseVeiwModel
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string Handle { get; set; }

        public SchemeType Type { get; set; }
       
        [Required]
        public List<FieldDefenitionAddOrUpdateModel> FieldDefenitions { get; set; }
    }
}
