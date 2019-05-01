using System;
using System.Data.Entity;
using System.Linq;
using DotLiquid;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.Menu;
using System.Collections.Generic;
using VarinaCmsV2.ViewModel;
using VarinaCmsV2.Core.Contracts.Widget;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.Common;
using VarinaCmsV2.ViewModel.Meta;

namespace VarinaCmsV2.Core.Logic.Widgets.Menu
{
    public class MenuWidget : BaseWidgetWithMetaData<MenuMetaData>
    {
        private readonly IMenuDataService _menuDataService;
        private readonly IMetaDataDataService _metaDataService;
        private readonly List<MenuItemLiquidDecorator> _decorators;
        public MenuWidget()
        {
            _menuDataService = Container.GetInstance<IMenuDataService>();
            _decorators = Container.GetInstance<List<MenuItemLiquidDecorator>>();
            _metaDataService = Container.GetInstance<IMetaDataDataService>();
        }

        public override string Type { get; } = "menu";
        public override BaseWidgetLiquidData Parse(JObject widgetMetaData)
        {
            base.DeserializeMetaData(widgetMetaData);
            var menu = _menuDataService.Query.AsNoTracking().Where(x => x.Id == MetaData.MenuId).Select(m => new
            {
                Menu = m,
                MenuItems = m.MenuItems.Where(i => !i.ParentId.HasValue).ToList(),
            }).First();

            if (menu != null)
            {
                var menuVm = menu.Menu.MapToLiquidViewModel();
                menuVm.MenuItems = GetDecorated(menu.MenuItems.ToLiquidList());
                var metas = GetMetas(menuVm.MenuItems.SelectDeep(x => x.Id));
                menuVm.MenuItems.ForEachDeep(x => x.Metas = metas.Where(m=>m.TargetId==x.Id).MapToLiquid());
                menuVm.ApplyItemsOrder();

                return new MenuWidgetLiquidData() { Menu = menuVm };
            }
            return null;
        }

        private List<Meta> GetMetas(IEnumerable<Guid> ids)
        {
            return _metaDataService
                .Query
                .Where(x => ids.Contains(x.TargetId) && x.TargetType == MenuItem.MetaTypeName)
                .ToList();
        }

        private List<MenuItemLiquidViewModel> GetDecorated(List<MenuItemLiquidViewModel> items)
        {
            var list = new List<MenuItemLiquidViewModel>();
            items.ForEach(i =>
            {
                var decorated = new MenuItemLiquidViewModel();
                _decorators.ForEach(x =>
                {
                    decorated = x.Decorate(i);
                });
                if (i.Childs != null && i.Childs.Count > 0)
                {
                    decorated.Childs = GetDecorated(i.Childs);
                }
                list.Add(decorated);
            });
            return list;
        }
    }
}
