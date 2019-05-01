using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Meta;

namespace VarinaCmsV2.Core.Contracts.MetaData
{
    public interface IMetaDataHandler
    {
        Task HandleUpdatesAsync(IHaveMetaData item, List<MetaDataAddOrUpdateViewModel> updateds);
        Task HandleUpdatesAsync(List<Meta> exsitings, List<MetaDataAddOrUpdateViewModel> updateds);
        Task HandleAddition(List<MetaDataAddOrUpdateViewModel> metas);
    }
}
