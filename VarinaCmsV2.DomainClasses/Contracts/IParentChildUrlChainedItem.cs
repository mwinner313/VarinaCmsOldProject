using System;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.DomainClasses.Contracts
{
    public interface IParentChildUrlChainedItem<T> : IHaveCollectionChild<T> where T :class
    {
        string UrlSegment { get; set; }
        string Url { get; set; }
        string Title { get; set; }
        T Parent { get; set; }
        Guid? ParentId { get; set; }
    }
}
