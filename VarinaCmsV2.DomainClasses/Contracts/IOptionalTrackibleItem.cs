using System;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.DomainClasses.Contracts
{
    public interface IOptionalTrackibleItem
    {
        DateTime UpdateDateTime { get; set; }
        DateTime CreateDateTime { get; set; }
        User Creator { get; set; }
        Guid? CreatorId { get; set; }
    }
}
