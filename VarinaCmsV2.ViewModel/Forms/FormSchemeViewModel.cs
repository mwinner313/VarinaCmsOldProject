using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.ViewModel.Forms
{
    public class FormSchemeViewModel:BaseVeiwModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Handle { get; set; }
        public List<FieldDefenitionViewModel> FieldDefenitions { get; set; }
    }
}
