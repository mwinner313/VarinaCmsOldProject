using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class FieldMapHelper
    {
        public static FieldAddOrUpdateViewModel MapToAddOrUpdateViewModel(this Field field)
        {
            return Mapper.Map<FieldAddOrUpdateViewModel>(field);
        }

        public static void MapToExisting(this FieldAddOrUpdateViewModel viewModel,Field exsiting)
        {
             Mapper.Map(viewModel,exsiting);
        }

        public static List<Field> MapToModel(this IEnumerable<FieldAddOrUpdateViewModel> fields)
        {
            return Mapper.Map<List<Field>>(fields);
        }
    }
}
