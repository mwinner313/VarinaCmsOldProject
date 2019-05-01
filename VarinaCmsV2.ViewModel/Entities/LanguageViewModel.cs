using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.ViewModel.Entities
{
   public class LanguageViewModel:BaseVeiwModel
    {
        [StringLength(5,MinimumLength = 5)]
        public string Name { get; set; }
    }
}
