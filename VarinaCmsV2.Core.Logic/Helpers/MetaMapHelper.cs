using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel;
using VarinaCmsV2.ViewModel.Meta;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class MetaMapHelper
    {
        public static List<MetaDataViewModel> MapToViewModelList(this IEnumerable<Meta> metas)
        {
            return Mapper.Map<List<MetaDataViewModel>>(metas);
        }
        public static List<Meta> MapToModelList(this IEnumerable<MetaDataAddOrUpdateViewModel> metas)
        {
            return Mapper.Map<List<Meta>>(metas);
        }
        public static void MapToExistingModelList(this List<MetaDataAddOrUpdateViewModel> metas, List<Meta> existings)
        {
            metas.ForEach(x => Mapper.Map(x, existings.First(e => e.Id == x.Id)));
        }
        public static Meta MapToModel(this MetaDataAddOrUpdateViewModel meta)
        {
            return Mapper.Map<Meta>(meta);
        }
        public static MetaDataViewModel MapToViewModel(this Meta meta)
        {
            return Mapper.Map<MetaDataViewModel>(meta);
        }

        public static List<MetaDataLiquidViewMdoel> MapToLiquid(this IEnumerable<Meta> metas)
        {
            return Mapper.Map<List<MetaDataLiquidViewMdoel>>(metas);
        }
      
    }
}
