using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.LogicFacade
{
    public interface IFieldDefentionFacade
    {
       void  PendToAdd(FieldDefenition model);
        void PendToUpdate(FieldDefenition model);
        void PendToUpdate(List<FieldDefenition> model);
        void PendToDelete(Guid id);
        Task SaveChangesAsync();
        void IgnoreChanges();
    }
}
