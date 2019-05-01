using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.MetaData;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Meta;

namespace VarinaCmsV2.Core.Logic.MetaData
{
    public class MenuItemMetaDataHandler : IMenuItemMetaDataHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMetaDataDataService _metaDataService;


        public MenuItemMetaDataHandler(IUnitOfWork unitOfWork, IMetaDataDataService metaDataService)
        {
            _unitOfWork = unitOfWork;
            _metaDataService = metaDataService;
        }

        public async Task HandleUpdatesAsync(IHaveMetaData item, List<MetaDataAddOrUpdateViewModel> updateds)
        {
            var oldMetas =
                _metaDataService.Query.Where(x => x.TargetId == item.Id && x.TargetType == MenuItem.MetaTypeName).ToList();
            await HandleUpdatesAsync(oldMetas, updateds);
        }
        public async Task HandleUpdatesAsync(List<Meta> exsitings, List<MetaDataAddOrUpdateViewModel> updateds)
        {
            exsitings.Where(x => !updateds.Select(u => u.Id).Contains(x.Id)).ToList().ForEach(_unitOfWork.Delete);
            updateds.Where(x => x.Id == Guid.Empty).ToList().ForEach(ValidateDataAndAdd);
            updateds.Where(x => exsitings.Select(e => e.Id).Contains(x.Id) && x.HasChanges)
                .ToList()
                .ForEach(x => ValidateAndUpdate(x.MapToModel(), exsitings.First(e => e.Id == x.Id)));

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task HandleAddition(List<MetaDataAddOrUpdateViewModel> metas)
        {
            await _unitOfWork.SaveChangesAsync();
        }

        private void ValidateAndUpdate(Meta updated, Meta old)
        {
            // todo validate base on metaname 
            old.MetaData = updated.MetaData;
            _unitOfWork.Update(old);
        }


        private void ValidateDataAndAdd(MetaDataAddOrUpdateViewModel metaViewModel)
        {
            // todo validate base on metaname 
            var meta = metaViewModel.MapToModel();
            meta.TargetType = MenuItem.MetaTypeName;
            _unitOfWork.Add(meta);
        }

     
    }
}