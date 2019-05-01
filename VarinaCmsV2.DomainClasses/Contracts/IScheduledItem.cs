using System;

namespace VarinaCmsV2.DomainClasses.Contracts
{
    public interface IScheduledItem
    {
         DateTime PublishDateTime { get; set; }
    }
}
