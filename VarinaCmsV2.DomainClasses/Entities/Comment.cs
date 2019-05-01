using System;
using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.DomainClasses.Entities
{
    //TODO Create Comment Config
    public class Comment:BaseEntity,IOptionalTrackibleItem
    {
        public string Text { get; set; }
        public string Email { get; set; }
        public bool Approved { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public User Creator { get; set; }
        public Guid? CreatorId { get; set; }
        public Guid? ApproverId { get; set; }
        public User Approver { get; set; }
        public Guid? ParentId { get; set; }
        public Guid TargetId { get; set; }
        public CommentTargetType TargetType { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual ICollection<Comment> Childs { get; set; }
//        public Portal Portal { get; set; }
//        public Guid PortalId { get; set; }
    }
}
