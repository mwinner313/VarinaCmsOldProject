using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Menu;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class MenuMapHelper
    {
        public static Menu MapToModel(this MenuAddUpdateViewModel menu)
        {
            return Mapper.Map<Menu>(menu);
        }

        public static Menu MapToModel(this MenuAddUpdateViewModel menu, Menu existing)
        {
            return Mapper.Map(menu, existing);
        }

        public static MenuViewModel MapToViewModel(this Menu menu)
        {
            return Mapper.Map<MenuViewModel>(menu);
        }

        public static async Task<List<MenuViewModel>> WithItemsMetaDataToViewModelListAsync(this IQueryable<Menu> queriableMenus,
            IQueryable<MenuItem> menuItemsQueryable, IQueryable<Meta> metas)
        {
            var menus =
                await queriableMenus.Select(
                    x =>
                        new
                        {
                            Menu = x,
                            MenuItems = menuItemsQueryable.Where(i => i.MenuId == x.Id && i.ParentId == null).OrderBy(i=>i.Order),
                        }).ToListAsync();
            var menusList = menus.Select(x => x.Menu).ToList();
            menusList.ForEach(x => x.MenuItems.Clear());
            var vm = Mapper.Map<List<MenuViewModel>>(menusList);

            vm.ForEach(m => m.MenuItems = menus.First(x => x.Menu.Id == m.Id).MenuItems.ToViewModelList());
            vm.LoadItemsMetaData(metas);
            return vm;
        }


        public static void LoadItemsMetaData(this List<MenuViewModel> menus, IQueryable<Meta> metaQuery)
        {
            var allItemsIds = menus.Select(x => x.MenuItems.SelectDeep(i => i.Id)).SelectMany(x => x).ToList();
            var metas = metaQuery.Where(x => allItemsIds.Contains(x.TargetId) && x.TargetType == MenuItem.MetaTypeName).ToList();
            menus.ForEach(x => x.MenuItems.ForEachDeep(i => i.MetaData = metas.Where(m => m.TargetId == i.Id).MapToViewModelList()));
        }

        public static List<MenuItemViewModel> ToViewModelList(this IEnumerable<MenuItem> model)
        {
            return Mapper.Map<List<MenuItemViewModel>>(model.ToList());
        }

        public static List<MenuItem> MapToModel(this IEnumerable<MenuItemAddUpdateViewModel> model)
        {
            return Mapper.Map<List<MenuItem>>(model.ToList());
        }

        public static MenuItem MapToModel(this MenuItemAddUpdateViewModel model)
        {
            return Mapper.Map<MenuItem>(model);
        }

        public static MenuItemViewModel MapToViewModel(this MenuItem model)
        {
            return Mapper.Map<MenuItemViewModel>(model);
        }

        public static MenuItem MapToModel(this MenuItemAddUpdateViewModel model, MenuItem exsiting)
        {
            return Mapper.Map(model, exsiting);
        }

        public static MenuLiquidViewModel MapToLiquidViewModel(this Menu menu)
        {
            return Mapper.Map<MenuLiquidViewModel>(menu);
        }
        public static void ApplyItemsOrder(this MenuLiquidViewModel model)
        {
            model.MenuItems = model.MenuItems.OrderBy(x => x.Order).ToList();
            model.MenuItems.ForEach(x => x.ApplyChildsOrder());
        }
        public static void ApplyChildsOrder(this MenuItemLiquidViewModel model)
        {
            if (model.Childs != null)
            {
                model.Childs = model.Childs.OrderBy(x => x.Order).ToList();
                model.Childs.ForEach(x => x.ApplyChildsOrder());
            }
        }

        public static List<MenuItemLiquidViewModel> ToLiquidList(this IEnumerable<MenuItem> items)
        {
            return Mapper.Map<List<MenuItemLiquidViewModel>>(items);
        }
    }
}