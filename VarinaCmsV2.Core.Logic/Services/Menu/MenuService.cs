using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.MetaData;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Menu;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.ViewModel.Menu;
using Z.EntityFramework.Plus;

namespace VarinaCmsV2.Core.Logic.Services.Menu
{
    public class MenuService : BaseService<DomainClasses.Entities.Menu, MenuResponse, MenuListResponse>, IMenuService
    {
        private readonly IMenuDataService _menuDataSrv;
        private readonly IDbSet<MenuItem> _menuItemsDataSet;
        private readonly IMenuItemMetaDataHandler _metaDataHandler;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMetaDataDataService _metaDataService;
        public MenuService(List<IBaseBeforeAddingEntityLogic> baseBeforeAddingEntityLogics,
            List<BaseAfterAddingEntityLogic> baseAfterAddingEntityLogics,
            List<IBaseAfterUpdatingEntityLogic> baseAfterUpdateEntityLogics,
            List<IBaseBeforeUpdatingEntityLogic> baseBeforeUpdateEntityLogics,
            List<IBaseBeforeDeleteEntityLogic> baseBeforeDeleteEntityLogics,
            List<BaseAfterDeleteEntityLogic> baseAfterDeleteEntityLogics, IIdentityManager identityManager,
            IRestrictedItemAccessManager accessManager, IMenuDataService dataSrv, IUnitOfWork unitOfWork,
            IMenuItemMetaDataHandler metaDataHandler, IMetaDataDataService metaDataService)
            : base(
                baseBeforeAddingEntityLogics, baseAfterAddingEntityLogics, baseAfterUpdateEntityLogics,
                baseBeforeUpdateEntityLogics, baseBeforeDeleteEntityLogics, baseAfterDeleteEntityLogics, identityManager,
                accessManager, dataSrv)
        {
            _menuDataSrv = dataSrv;
            _unitOfWork = unitOfWork;
            _metaDataHandler = metaDataHandler;
            _metaDataService = metaDataService;
            _menuItemsDataSet = _unitOfWork.Set<MenuItem>();
        }

        public async Task<MenuListResponse> Get(MenuListRequest request)
        {
            if (!HasPremission(request.RequestOwner, MenuPremssion.CanSee)) return UnauthorizedListRequest();
            var menus =
                await _menuDataSrv.Query
                    .OrderByDescending(x => x.Id)
                    .WithItemsMetaDataToViewModelListAsync(_menuItemsDataSet, _metaDataService.Query);
            return new MenuListResponse
            {
                Access = ResponseAccess.Granted,
                Menus = menus
            };
        }


        public async Task<MenuResponse> Add(MenuAddRequest request)
        {
            if (!HasPremission(request.RequestOwner, MenuPremssion.CanAdd)) return UnauthorizedRequest();
            var menuWithItems = request.ViewModel.MapToModel();
            await BaseBeforeAddAsync(menuWithItems, request.RequestOwner);
            await _menuDataSrv.AddAsync(menuWithItems);
            await request.ViewModel.MenuItems.ForEachAsync(x => _metaDataHandler.HandleAddition(x.MetaData));
            await BaseAfterAddAsync(menuWithItems, request.RequestOwner);
            return new MenuResponse
            {
                Access = ResponseAccess.Granted,
                Menu = menuWithItems.MapToViewModel()
            };
        }

        public async Task<MenuResponse> Update(MenuUpdateRequest request)
        {
            if (!HasPremission(request.RequestOwner, MenuPremssion.CanUpdate)) return UnauthorizedRequest();
            var updatingMenu = await _menuDataSrv.Query.FirstAsync(x => x.Id == request.MenuId);
            request.ViewModel.MapToModel(updatingMenu);
            await BaseBeforeUpdateAsync(updatingMenu, request.RequestOwner);
            await HandleMenuItemsAsync(request, updatingMenu);
            await _menuDataSrv.UpdateAsync(updatingMenu);
            await HandleMetaDataAsync(updatingMenu.MenuItems.ToList(), request.ViewModel.MenuItems);
            await BaseAfterUpdateAsync(updatingMenu, request.RequestOwner);
            return new MenuResponse
            {
                Access = ResponseAccess.Granted
            };
        }

        public async Task<MenuResponse> Delete(MenuDeleteRequest request)
        {
            if (!HasPremission(request.RequestOwner, MenuPremssion.CanAdd)) return UnauthorizedRequest();
            var deletingMenu = _menuDataSrv.Query.First(x => x.Id == request.MenuId);
            await BaseBeforeDeleteAsync(deletingMenu, request.RequestOwner);
            await _menuDataSrv.DeleteAsync(deletingMenu.Id);
            await BaseAfterDeleteAsync(deletingMenu, request.RequestOwner);
            return new MenuResponse
            {
                Access = ResponseAccess.Granted
            };
        }

        public async Task<List<MenuItemViewModel>> AddSingleMenuItem(List<MenuItemAddUpdateViewModel> model)
        {
            var res = new List<MenuItemViewModel>();
            var set = _unitOfWork.Set<MenuItem>();
            foreach (var item in model)
            {
                item.Order=9999;
                if (item.TargetType != MenuItemTargetType.CustomLink)
                    item.Url = "";
                var added = set.Add(item.MapToModel());
                await _unitOfWork.SaveChangesAsync();
                res.Add(added.MapToViewModel());
            }
            return res;
        }


        private async Task HandleMetaDataAsync(List<MenuItem> updatingMenuMenuItems, List<MenuItemAddUpdateViewModel> updated)
        {
            await updated.ForEachAsync(
                   x => _metaDataHandler.HandleUpdatesAsync(updatingMenuMenuItems.First(i => i.Id == x.Id), x.MetaData));
        }

        private async Task HandleMenuItemsAsync(MenuUpdateRequest request, DomainClasses.Entities.Menu updatingMenu)
        {
            var menuItemSet = _unitOfWork.Set<MenuItem>();
           await DeleteItems(request, updatingMenu, menuItemSet);
            UpdateItems(request, menuItemSet);
        }

        private async Task DeleteItems(MenuUpdateRequest request, DomainClasses.Entities.Menu updatingMenu,
            DbSet<MenuItem> menuItemSet)
        {
            var receviedMenuItemIds = request.ViewModel.MenuItems.Select(x => x.Id);

            var deletedMenuItems =
                menuItemSet.Where(x => !receviedMenuItemIds.Contains(x.Id) && x.MenuId == updatingMenu.Id).ToList();

            await deletedMenuItems.ForEachAsync(async x =>
             {
                 _unitOfWork.Delete(x);
                 await base.BaseAfterDeleteAsync(x, request.RequestOwner);
             });
        }

        private void UpdateItems(MenuUpdateRequest request, IDbSet<MenuItem> menuItemSet)
        {
            var updatedMenuItems = request.ViewModel.MenuItems.Where(x => x.Id != Guid.Empty).ToList();
            foreach (var item in updatedMenuItems)
            {
                var updated = menuItemSet.Single(x => x.Id == item.Id);
                if (item.TargetType != MenuItemTargetType.CustomLink)
                    item.Url = "";
                item.MapToModel(updated);
                _unitOfWork.Update(updated);
            }
        }
    }
}