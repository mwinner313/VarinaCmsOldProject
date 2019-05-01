using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class EntitySchemeHelper
    {
        public static EntityScheme MapToEntity(this EntitySchemeViewModel model)
        {
            return Mapper.Map<EntityScheme>(model);
        }
        public static EntityScheme MapToExisting(this EntitySchemeUpdateViewModel viewModel, EntityScheme model)
        {
            return Mapper.Map(viewModel, model);
        }
        public static EntitySchemeViewModel MapToViewModel(this EntityScheme model)
        {
            return Mapper.Map<EntitySchemeViewModel>(model);
        }
        public static List<EntitySchemeViewModel> MapToViewModel(this IEnumerable<EntityScheme> model)
        {
            return Mapper.Map<List<EntitySchemeViewModel>>(model);
        }
        public static async Task<List<EntitySchemeViewModel>> ToViewModelListAsync(this IQueryable<EntityScheme> pages)
        {
            return Mapper.Map<List<EntityScheme>, List<EntitySchemeViewModel>>(await pages.ToListAsync());
        }
    }
}
