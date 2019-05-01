using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.Contracts
{
    public interface ICommentibleItem
    {
        ICollection<Comment> Comments { get; set; }
        bool IsCommentDisabled { get; set; }
    }
}
