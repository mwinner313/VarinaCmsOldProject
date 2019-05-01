using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.ViewModel.Entities
{
    public class FieldDefenitionGroupViewModel: BaseVeiwModel
    {
        public List<FieldDefenitionViewModel> FieldDefenitions { get; set; }
        public string Title { get; set; }
        public Guid? SchemeId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? ProductCategoryId { get; set; }
    }
}
