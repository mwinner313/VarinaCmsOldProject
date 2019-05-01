using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Forms;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class FormSchemeMapHelper
    {
        public static async Task<List<FormSchemeViewModel>> ToViewModelListAsync(this IQueryable<FormScheme> model)
        {
            return Mapper.Map<List<FormSchemeViewModel>>(await model.ToListAsync());
        }
        public static FormSchemeViewModel MapToViewModel(this FormScheme model)
        {
            return Mapper.Map<FormSchemeViewModel>(model);
        }
        public static FormScheme MapToModel(this FormSchemeAddOrUpdateViewModel model)
        {
            return Mapper.Map<FormScheme>(model);
        }
        public static FormScheme MapToModel(this FormSchemeAddOrUpdateViewModel model, FormScheme existing)
        {
            return Mapper.Map(model, existing);
        }

    }
}
