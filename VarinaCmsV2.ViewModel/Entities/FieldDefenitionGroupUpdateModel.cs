using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.ViewModel.Entities
{
    public class FieldDefenitionGroupAddOrUpdateModel:BaseVeiwModel
    {
        [Required]
        public string Title { get; set; }
        public Guid? SchemeId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? ProductCategoryId { get; set; }
        public List<FieldDefenitionViewModel> FieldDefenitions { get; set; }
    }
}
