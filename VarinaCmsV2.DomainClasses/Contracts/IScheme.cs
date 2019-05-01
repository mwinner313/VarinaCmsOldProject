using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.Contracts
{
    public interface IScheme
    {
         ICollection<FieldDefenition> FieldDefenitions { get; set; }
         ICollection<FieldDefenitionGroup> FieldDefenitionGroups { get; set; }
         string Url { get; set; }
        string Handle { get; set; }
        string Title { get; set; }
    }
}