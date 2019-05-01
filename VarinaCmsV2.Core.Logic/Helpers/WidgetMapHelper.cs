using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities.Widgets;
using VarinaCmsV2.ViewModel.Widget;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class WidgetMapHelper
    {
        public static List<WidgetContainerViewModel> MapToViewModel(this IEnumerable<WidgetContainer> containers)
        {
            return AutoMapper.Mapper.Map<List<WidgetContainerViewModel>>(containers);
        }

        public static Widget MapToModel(this WidgetAddOrUpdateViewModel viewModel) => Mapper.Map<Widget>(viewModel);
        public static Widget MapToModel(this WidgetAddOrUpdateViewModel viewModel, Widget existing) => Mapper.Map(viewModel, existing);
        public static WidgetViewModel MapToViewModel(this Widget model) => Mapper.Map<WidgetViewModel>(model);

        public static WidgetContainer MapToModel(this WidgetContainerAddOrUpdateViewModel viewModel) => Mapper.Map<WidgetContainer>(viewModel);
        public static WidgetContainer MapToModel(this WidgetContainerAddOrUpdateViewModel viewModel, WidgetContainer existing) => Mapper.Map(viewModel, existing);
        public static WidgetContainerViewModel MapToViewModel(this WidgetContainer model) => Mapper.Map<WidgetContainerViewModel>(model);
        public static WidgetAddOrUpdateViewModel MapToAddOrUpdateViewModel(this Widget model) => Mapper.Map<WidgetAddOrUpdateViewModel>(model);
    }
}
