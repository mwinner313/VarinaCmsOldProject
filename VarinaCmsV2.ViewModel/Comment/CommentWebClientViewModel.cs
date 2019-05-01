using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.ViewModel.Comment
{
    public class CommentWebClientViewModel:BaseVeiwModel
    {
        public string Text { get; set; }
        public string Email { get; set; }
        public Guid TargetId { get; set; }
        public UserWebClientViewModel Creator { get; set; }
        public DateTime CreateDateTime { get; set; }
        public Guid? CreatorId { get; set; }
    }
}
