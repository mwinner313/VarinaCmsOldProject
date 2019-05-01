using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;

namespace VarinaCmsV2.ViewModel
{

    public class ParentChildIdViewModel
    {
        [Required]
        public Guid Id { get; set; }
       
        public Guid? ParentId { get; set; }
        [Required]
        public int Order { get; set; }

        public List<ParentChildIdViewModel> Childs { get; set; }
    }
}
