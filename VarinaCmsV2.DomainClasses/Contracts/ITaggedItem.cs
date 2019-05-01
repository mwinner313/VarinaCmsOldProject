using System;
using System.Collections.Generic;

using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.Contracts
{
    public interface ITaggedItem
    {
         ICollection<Tag> Tags { get; set; }
         Guid Id { get; set; }
    }
}
