using System.Collections.Generic;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Contracts.Builders
{
    public interface ISchemeBuilder<T> where T : IScheme
    {
        IUrlBuilder UrlBuilder { get; set; } 
        void AddFieldDefenition(FieldDefenition model);
        void  AddFieldDefenition(List<FieldDefenition> model);
        void SetBuildingScheme(T scheme);
         T GetResult();
        void AddFieldDefenitionGroup(List<FieldDefenitionGroup> fieldDefenitionGroups);
    }
}
