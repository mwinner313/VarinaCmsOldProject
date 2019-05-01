using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.Services.Page;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Security;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.ViewModel.Page;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Logic.Services.Page
{
    public class PageService : BaseService<DomainClasses.Entities.Page, PageResponse, PageListResponse>, IPageService
    {
        private readonly IPageDataService _dataSrv;
        private readonly ISecurityLogger _securityLogger;
        private readonly IUrlBuilder _urlBuilder;
        private readonly IUnitOfWork _unitOfWork;

        public PageService(List<IBaseBeforeAddingEntityLogic> baseBeforeAddingEntityLogics,
            List<BaseAfterAddingEntityLogic> baseAfterAddingEntityLogics,
            List<IBaseAfterUpdatingEntityLogic> baseAfterUpdateEntityLogics,
            List<IBaseBeforeUpdatingEntityLogic> baseBeforeUpdateEntityLogics,
            List<IBaseBeforeDeleteEntityLogic> baseBeforeDeleteEntityLogics,
            List<BaseAfterDeleteEntityLogic> baseAfterDeleteEntityLogics,
            IIdentityManager identityManager,
            IRestrictedItemAccessManager accessManager,
            IPageDataService dataSrv,
            ISecurityLogger securityLogger, IUrlBuilder urlBuilder, IUnitOfWork unitOfWork) :

            base(baseBeforeAddingEntityLogics, baseAfterAddingEntityLogics, baseAfterUpdateEntityLogics, baseBeforeUpdateEntityLogics, baseBeforeDeleteEntityLogics, baseAfterDeleteEntityLogics, identityManager, accessManager, dataSrv)
        {
            _dataSrv = dataSrv;
            _securityLogger = securityLogger;
            _urlBuilder = urlBuilder;
            _unitOfWork = unitOfWork;
        }

        public async Task<PageResponse> Get(PageRequest request)
        {
            var page = await _dataSrv.GetAsync(request.PageId);

            if (!HasAccessToSee(page, request.RequestOwner)) return UnauthorizedRequest();

            return new PageResponse() { Access = ResponseAccess.Granted, Page = page.MapForShowViewModel() };
        }

        public async Task<PageListResponse> Get(PageListRequest request)
        {
            var allowedItems  = GetFilteredQueryByReqParamsAndUserAccess(request);
            var totalRequestItemCount = allowedItems.Count();
            allowedItems = allowedItems.Paginate( request.Query??new Pagenation());
            return new PageListResponse()
            {
                Access = ResponseAccess.Granted,
                Pages = await allowedItems.ToViewModelListAsync(),
                Count = totalRequestItemCount
            };
        }

        private IQueryable<DomainClasses.Entities.Page> GetFilteredQueryByReqParamsAndUserAccess(PageListRequest request)
        {
            var pageQuery = AccessManager.Filter(_dataSrv.Query.Where(x => x.LanguageName == request.Query.LanguageName));
            var searchText = request.Query?.SearchText;

            if (!string.IsNullOrEmpty(searchText))
                pageQuery = pageQuery.Where(x => x.Title.Contains(searchText) );

            pageQuery = pageQuery.OrderByDescending(x => x.Id);
            return pageQuery;
        }

        public async Task<PageResponse> Add(PageAddRequest request)
        {
            if (!HasPremission(request.RequestOwner, PagePremision.CanManage))
                return UnauthorizedRequest("UnAuthorized to add page");

            var model = request.ViewModel.MapToModel();

            //model.UrlSegment = string.IsNullOrEmpty(model.Url)
            //    ? _urlBuilder.GenrateUrlSegment(model.Title)
            //    : _urlBuilder.GenrateUrlSegment(model.UrlSegment);

            await BaseBeforeAddAsync(model, request.RequestOwner);
            await _dataSrv.AddAsync(model);
            await BaseAfterAddAsync(model, request.RequestOwner);

            return Success();
        }

        public async Task<PageResponse> Update(PageUpdateRequest request)
        {
            if (!HasPremission(request.RequestOwner, PagePremision.CanManage))
                return UnauthorizedRequest("UnAuthorized to update page");

            var updatingPage = await _dataSrv.GetAsync(request.PageId);

            if (!HasAccessToManage(updatingPage, request.RequestOwner))
            {
                _securityLogger.LogDangeriousUpdateAttemp(request.RequestOwner, updatingPage);
                return UnauthorizedRequest("UnAuthorized to update this Page page");
            }

            await UpdatePage(request, updatingPage);
            return Success();
        }

        private async Task UpdatePage(PageUpdateRequest request, DomainClasses.Entities.Page updatingPage)
        {
            request.ViewModel.MapTo(updatingPage);

            await BaseBeforeUpdateAsync(updatingPage, request.RequestOwner);
            await _dataSrv.UpdateAsync(updatingPage);
            await BaseAfterUpdateAsync(updatingPage, request.RequestOwner);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PageResponse> Delete(PageDeleteRequest request)
        {
            if (!HasPremission(request.RequestOwner, PagePremision.CanManage))
                return UnauthorizedRequest("UnAuthorized to update page");

            var deletingPage = await _dataSrv.GetAsync(request.PageId);

            if (!HasAccessToManage(deletingPage, request.RequestOwner))
            {
                _securityLogger.LogDangeriousUpdateAttemp(request.RequestOwner, deletingPage);
                return UnauthorizedRequest("UnAuthorized to update this Page page");
            }
            await base.BaseBeforeDeleteAsync(deletingPage, request.RequestOwner);
            await _dataSrv.DeleteAsync(request.PageId);
            await base.BaseAfterDeleteAsync(deletingPage, request.RequestOwner);
            return Success();
        }


    }
}
