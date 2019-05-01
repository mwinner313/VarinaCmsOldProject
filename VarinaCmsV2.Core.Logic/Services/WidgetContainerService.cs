using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Contracts.Widget;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Widget;
using VarinaCmsV2.DomainClasses.Entities.Widgets;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Logic.Services
{
    public class WidgetContainerService : BaseService<WidgetContainer, WidgetContainerResponse, WidgetContainerListResponse>,
        IWidgetContainerService
    {
        private readonly IWidgetFactory _widgetFactory;
        private readonly IWidgetDataService _widgetDataService;
        private readonly IWidgetContainerDataService _widgetContainerDataSrv;
        public WidgetContainerService(List<IBaseBeforeAddingEntityLogic> baseBeforeAddingEntityLogics,
            List<BaseAfterAddingEntityLogic> baseAfterAddingEntityLogics,
            List<IBaseAfterUpdatingEntityLogic> baseAfterUpdateEntityLogics,
            List<IBaseBeforeUpdatingEntityLogic> baseBeforeUpdateEntityLogics,
            List<IBaseBeforeDeleteEntityLogic> baseBeforeDeleteEntityLogics,
            List<BaseAfterDeleteEntityLogic> baseAfterDeleteEntityLogics, IIdentityManager identityManager,
            IRestrictedItemAccessManager accessManager, IWidgetContainerDataService widgetContainerDataSrv, IWidgetDataService widgetDataService, IWidgetFactory widgetFactory)
            : base(
                baseBeforeAddingEntityLogics, baseAfterAddingEntityLogics, baseAfterUpdateEntityLogics,
                baseBeforeUpdateEntityLogics, baseBeforeDeleteEntityLogics, baseAfterDeleteEntityLogics, identityManager,
                accessManager, widgetContainerDataSrv)
        {
            _widgetContainerDataSrv = widgetContainerDataSrv;
            _widgetDataService = widgetDataService;
            _widgetFactory = widgetFactory;
        }

        public async Task<WidgetContainerListResponse> Get(WidgetContainerGetListRequest listRequest)
        {

            var containers = await _widgetContainerDataSrv.Query.Include(x => x.Widgets).ToListAsync();
            containers.ForEach(x => { x.Widgets = x.Widgets.OrderBy(w => w.Order).ToList(); });

            return new WidgetContainerListResponse()
            {
                Access = ResponseAccess.Granted,
                WidgetContainers = containers.MapToViewModel(),
                AvailibleWidgets = _widgetFactory.GetAvailibleWidgets()
            };

        }

        public async Task<WidgetContainerResponse> Add(WidgetContainerAddRequest request)
        {
            var newContainer = request.Model.MapToModel();
            await _widgetContainerDataSrv.AddAsync(newContainer);
            return new WidgetContainerResponse() { Access = ResponseAccess.Granted, Model = newContainer.MapToViewModel() };
        }

        public async Task<WidgetContainerResponse> Edit(WidgetContainerEditRequest request)
        {
            var updating = _widgetContainerDataSrv.Query.First(x => x.Id == request.WidgetContainerId);
            request.Model.MapToModel(updating);
            await _widgetContainerDataSrv.UpdateAsync(updating);
            await request.Model.Widgets.ToList().ForEachAsync(x => EditWidget(new WidgetEditRequest()
            {
                RequestOwner = request.RequestOwner,
                Model = x,
                WidgetId = x.Id
            }));
            return new WidgetContainerResponse() { Access = ResponseAccess.Granted, Model = updating.MapToViewModel() };
        }

        public async Task<WidgetContainerResponse> Delete(WidgetContainerDeleteRequest request)
        {
            await _widgetContainerDataSrv.DeleteAsync(request.WidgetContainerId);
            return Success();
        }

        public async Task<WidgetResponse> AddWidget(WidgetAddRequest request)
        {
            var newWidget = request.Model.MapToModel();
            await _widgetDataService.AddAsync(newWidget);
            return new WidgetResponse() { Access = ResponseAccess.Granted, Model = newWidget.MapToViewModel() };
        }

        public async Task<WidgetResponse> EditWidget(WidgetEditRequest request)
        {
            var updating = _widgetDataService.Query.First(x => x.Id == request.WidgetId);
            request.Model.MapToModel(updating);
            await _widgetDataService.UpdateAsync(updating);
            return new WidgetResponse() { Access = ResponseAccess.Granted, Model = updating.MapToViewModel() };
        }

        public async Task<WidgetResponse> DeleteWidget(WidgetDeleteRequest request)
        {
            await _widgetDataService.DeleteAsync(request.WidgetId);
            return new WidgetResponse() { Access = ResponseAccess.Granted };
        }
    }
}