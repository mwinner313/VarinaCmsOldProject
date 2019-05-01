using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Forms;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class FormMapHelper
    {
        public static async Task<List<FormViewModel>> ToViewModelListAsync(this IQueryable<Form> forms)
        {
            return AutoMapper.Mapper.Map<List<FormViewModel>>(await forms.ToListAsync());
        }
    }
}
