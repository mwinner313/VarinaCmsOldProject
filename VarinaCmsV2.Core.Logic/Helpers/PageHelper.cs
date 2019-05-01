using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Page;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class PageHelper
    {
        public static Page MapToModel(this PageViewModel viewModel)
        {
            var model = Mapper.Map<Page>(viewModel);
            return model;
        }
        public static Page MapToModel(this PageAddUpdateViewModel viewModel)
        {
            var model = Mapper.Map<Page>(viewModel);
            return model;
        }
        public static void MapTo(this PageViewModel viewModel, Page model)
        {
            Mapper.Map(viewModel, model);
        }
        public static void MapTo(this PageAddUpdateViewModel viewModel, Page model)
        {
            Mapper.Map(viewModel, model);
        }
        public static PageAddUpdateViewModel MapToAddOrUpdateViewModel(this Page model)
        {
            return Mapper.Map<PageAddUpdateViewModel>(model);
        }
        public static PageViewModel MapForShowViewModel(this Page model)
        {
            return Mapper.Map<PageViewModel>(model);
        }

        public static async Task<List<PageViewModel>> ToViewModelListAsync(this IQueryable<Page> pages)
        {
            return Mapper.Map<List<Page>, List<PageViewModel>>(await pages.ToListAsync());
        }

        public static PageLiquidViewModel AdaptToLiquid(this Page page)
        {
            return Mapper.Map<PageLiquidViewModel>(page);
        }


    }
}
