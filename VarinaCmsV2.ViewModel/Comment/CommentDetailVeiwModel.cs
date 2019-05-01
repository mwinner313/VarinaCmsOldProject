using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.Page;

namespace VarinaCmsV2.ViewModel.Comment
{
    public class CommentDetailVeiwModel:CommentViewModel
    {
        public PageViewModel Page { get; set; }
        public EntityViewModel Entity { get; set; }
        public CommentViewModel Parent { get; set; }
    }
}
