using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Comments;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Logic.Services.Comment
{
    public class CommentService : BaseService<DomainClasses.Entities.Comment, CommentResponse, CommentListResponse>,
        ICommentService
    {
        private readonly ICommentDataService _commentData;
        private readonly IEntityDataService _entityDataService;
        private readonly IPageDataService _pageDataService;
        private readonly IUnitOfWork _unitOfWork;

        public async Task<CommentListResponse> GetList(CommentGetListRequest request)
        {
            var comments = _commentData
                .Query.Include(x => x.Creator).Include(x => x.Approver)
                .Filter(request.Query)
                .OrderBy(x => x.CreateDateTime);
            return new CommentListResponse()
            {
                Access = ResponseAccess.Granted,
                Items = (await comments.Paginate(request.Query).ToListAsync()).MapToViewModel(),
                Count = await comments.CountAsync()
            };
        }

        public async Task<CommentApproveRespnose> Approve(CommentApproveRequest request)
        {
            var changeds = new List<DomainClasses.Entities.Comment>();
            request.Ids.ForEach(async id =>
            {
                var approving = _commentData.Query.First(x => x.Id == id);
                approving.Approved = !approving.Approved;
                await BaseBeforeUpdateAsync(approving, request.RequestOwner);
                _unitOfWork.Update(approving);
                changeds.Add(approving);
            });
            await _unitOfWork.SaveChangesAsync();
            changeds.ForEach(async x => await BaseAfterDeleteAsync(x, request.RequestOwner));
            return new CommentApproveRespnose()
            {
                Access = ResponseAccess.Granted
            };
        }

        public async Task<CommentDeleteResponse> Delete(CommentDeleteRequest request)
        {
            //foreach (var id in request.Ids)
            //{
            //    var deleting = _commentData.Query.First(x => x.Id == id);
            //    await base.BaseBeforeDeleteAsync(deleting, request.RequestOwner);
            //    await _commentData.DeleteAsync(id);
            //    await base.BaseAfterDeleteAsync(deleting, request.RequestOwner);
            //}

            await _commentData.DeleteAsync(request.Ids);

            return new CommentDeleteResponse()
            {
                Access = ResponseAccess.Granted
            };
        }

        public Task<CommentEditResponse> Edit(CommentEditRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<CommentDetailResponse> GetDetial(CommentDetailRequest request)
        {
            var comment = await _commentData.GetAsync(request.Id);
            switch (comment.TargetType)
            {
                case CommentTargetType.Entity:
                    return new CommentDetailResponse()
                    {
                        Access = ResponseAccess.Granted,
                        Comment = comment.MapToViewModelWithTarget(await _entityDataService.Query.Include(x => x.Scheme).FirstAsync(x => x.Id == comment.TargetId))
                    };
                case CommentTargetType.Page:
                    return new CommentDetailResponse()
                    {
                        Access = ResponseAccess.Granted,
                        Comment = comment.MapToViewModelWithTarget(await _pageDataService.GetAsync(comment.TargetId))
                    };

                case CommentTargetType.Undefined:
                default: return new CommentDetailResponse() { Access = ResponseAccess.BadRequest };
            }
        }

        public CommentService(List<IBaseBeforeAddingEntityLogic> baseBeforeAddingEntityLogics,
            List<BaseAfterAddingEntityLogic> baseAfterAddingEntityLogics,
            List<IBaseAfterUpdatingEntityLogic> baseAfterUpdateEntityLogics,
            List<IBaseBeforeUpdatingEntityLogic> baseBeforeUpdateEntityLogics,
            List<IBaseBeforeDeleteEntityLogic> baseBeforeDeleteEntityLogics,
            List<BaseAfterDeleteEntityLogic> baseAfterDeleteEntityLogics, IIdentityManager identityManager,
            IRestrictedItemAccessManager accessManager, IDataService<DomainClasses.Entities.Comment, Guid> dataSrv,
            ICommentDataService commentData, IUnitOfWork unitOfWork, IEntityDataService entityDataService, IPageDataService pageDataService) :
            base(baseBeforeAddingEntityLogics, baseAfterAddingEntityLogics, baseAfterUpdateEntityLogics,
                baseBeforeUpdateEntityLogics, baseBeforeDeleteEntityLogics, baseAfterDeleteEntityLogics,
                identityManager, accessManager, dataSrv)
        {
            _commentData = commentData;
            _unitOfWork = unitOfWork;
            _entityDataService = entityDataService;
            _pageDataService = pageDataService;
        }
    }
}