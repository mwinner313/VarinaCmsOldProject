using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.ViewModel.Forms
{
    public class FormViewModel:BaseVeiwModel
    {
        public string UpdateDateTime { get; set; }
        public string FormSchemeTitle { get; set; }
        public string FormSchemeHandle { get; set; }
        public Guid FormSchemeId { get; set; }
        public string CreateDateTime { get; set; }
        public string CreatorUserName { get; set; }
        public bool IsSeen { get; set; }
        public List<FieldViewModel> Fields { get; set; }
    }
}
