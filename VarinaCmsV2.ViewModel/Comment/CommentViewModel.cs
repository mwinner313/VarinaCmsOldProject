using System;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.ViewModel.Comment
{
    public class CommentViewModel : BaseVeiwModel
    {
        public string Text { get; set; }
        public Guid TargetId { get; set; }
        public bool Approved { get; set; }
        public UserWebClientViewModel Creator { get; set; }
        public UserWebClientViewModel Approver { get; set; }
        public string Email { get; set; }
        public Guid? ApproverId { get; set; }
        public string UpdateDateTime { get; set; }
        public string CreateDateTime { get; set; }
        public CommentTargetType TargetType { get; set; }
        public Guid? CreatorId { get; set; }
    }
}