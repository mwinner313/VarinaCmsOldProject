using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Security.Premissions
{
    public class CommentPrmission:IPremissionList
    {
        public const string CanSee = "comment.see";
        public const string CanEdit = "comment.edit";
        public const string CanDelete = "comment.delete";
        public const string CanChangeApproveState = "comment.approve";
        public List<string> List { get; } = new List<string>()
        {
            CanSee         ,
            CanEdit        ,
            CanDelete,
            CanChangeApproveState
        };
    }
}
