using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Contracts.WebClientServices.Entities;
using VarinaCmsV2.Core.FrontEndOptions;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Services.WebClientServices
{
    public class EntityWebClientService : IEntityWebClientService
    {
        private readonly IRestrictedItemAccessManager _accessManager;
        private readonly IDbSet<Comment> _comments;
        private readonly int _pageSize = FrontEndDeveloperOptions.Instance.Pagination.Default;
        private readonly IDbSet<Entity> _query;
        private readonly IDbSet<EntityScheme> _schemes;
        private readonly IUnitOfWork _unitOfWork;

        public EntityWebClientService(IUnitOfWork unitOfWork, IRestrictedItemAccessManager accessManager)
        {
            _unitOfWork = unitOfWork;
            _accessManager = accessManager;
            _query = _unitOfWork.Set<Entity>();
            _schemes = _unitOfWork.Set<EntityScheme>();
            _comments = _unitOfWork.Set<Comment>();
        }

        public async Task<Entity> GetByUrlAndSchemeUrlAsync(string entityUrl, string schemeUrl)
        {
            var model = await _query
                .Include(x => x.PrimaryCategory)
                .Include(x => x.Fields)
                .Include(x => x.RelatedCategories)
                .Include(x => x.Creator)
                .Include(x => x.Tags)
                .Include(x => x.Scheme)
                .Include(x => x.Fields.Select(f => f.FieldDefenition))
                .FirstOrDefaultAsync(x => x.Scheme.Url == schemeUrl && x.Url == entityUrl);

            LoadComments(model);

            return model;
        }

        public async Task<PaginatedEntities> GetByTagAsync(Tag tag, int pageNumber)
        {
            var query = _accessManager.Filter(_query
                    .Include(x => x.PrimaryCategory)
                    .Include(x => x.Fields)
                    .Include(x => x.RelatedCategories)
                    .Include(x => x.Creator)
                    .Include(x => x.Tags)
                    .Include(x => x.Fields.Select(f => f.FieldDefenition))
                    .Where(x => x.Tags.Any(t => t.Id == tag.Id)))
                .OrderByDescending(x => x.PublishDateTime);

            return new PaginatedEntities
            {
                AllPagesCount = query.GetAllPagesCount(FrontEndDeveloperOptions.Instance.Pagination.Default),
                CurrentPage = pageNumber,
                Entities = await query
                    .Paginate(_pageSize, pageNumber)
                    .ToListAsync()
            };
        }

        public async Task<PaginatedEntities> GetByCategoryAsync(Category category, int pageNumber)
        {
            var query = _accessManager.Filter(_query
                .Include(x => x.PrimaryCategory)
                .Include(x => x.Fields)
                .Include(x => x.Creator)
                .Include(x => x.RelatedCategories)
                .Include(x => x.Tags)
                .Include(x => x.Fields.Select(f => f.FieldDefenition))
                .Where(
                    x => x.PrimaryCategoryId == category.Id ||
                         x.RelatedCategories.Any(t => t.Id == category.Id))
            ).OrderByDescending(x => x.PublishDateTime);

            return new PaginatedEntities
            {
                AllPagesCount = query.GetAllPagesCount(FrontEndDeveloperOptions.Instance.Pagination.Default),
                CurrentPage = pageNumber,
                Entities = await query
                    .Skip((pageNumber - 1) * FrontEndDeveloperOptions.Instance.Pagination.Default)
                    .Take(FrontEndDeveloperOptions.Instance.Pagination.Default)
                    .ToListAsync()
            };
        }

        public async Task<UserEntitiesPaginated> GetByUserAsync(User user, string schemeUrl, int pageNumber)
        {
            var query = _accessManager.Filter(_query
                .Include(x => x.PrimaryCategory)
                .Include(x => x.Fields)
                .Include(x => x.Creator)
                .Include(x => x.RelatedCategories)
                .Include(x => x.Tags)
                .Include(x => x.Scheme)
                .Include(x => x.Fields.Select(f => f.FieldDefenition))
                .Where(x => x.CreatorId == user.Id));

            return new UserEntitiesPaginated
            {
                AllPagesCount = query.GetAllPagesCount(FrontEndDeveloperOptions.Instance.Pagination.Default),
                CurrentPage = pageNumber,
                Entities = await query.OrderByDescending(x => x.PublishDateTime)
                    .Skip((pageNumber - 1) * FrontEndDeveloperOptions.Instance.Pagination.Default)
                    .Take(FrontEndDeveloperOptions.Instance.Pagination.Default)
                    .ToListAsync(),
                User = user,
                Scheme = _schemes.First(x => x.Url == schemeUrl)
            };
        }

        public async Task<Entity> CountUpLike(Guid id, IPrincipal user)
        {
            var entity = _query.First(x => x.Id == id);
            entity.LikeCount++;
            _unitOfWork.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        public async Task<Entity> CountDownLike(Guid id, IPrincipal user)
        {
            var entity = _query.First(x => x.Id == id);
            entity.LikeCount--;
            _unitOfWork.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Entity>> SearchAsync(string schemeUrl, string searchText)
        {
            return await _accessManager.Filter(_query
                .Include(x => x.PrimaryCategory)
                .Include(x => x.Fields)
                .Include(x => x.RelatedCategories)
                .Include(x => x.Creator)
                .Include(x => x.Tags)
                .Include(x => x.Fields.Select(f => f.FieldDefenition))
                .Where(
                    x => x.Title.Contains(searchText) ||
                         x.Fields.Any(f => f.RawValueString.Contains(searchText))
                )).ToListAsync();
        }

        public async Task<PaginatedEntities> GetBySchemeAsync(EntityScheme scheme, int pageNumber)
        {
            var query = _accessManager.Filter(_query
                .Include(x => x.PrimaryCategory)
                .Include(x => x.Fields)
                .Include(x => x.Creator)
                .Include(x => x.RelatedCategories)
                .Include(x => x.Tags)
                .Include(x => x.Scheme)
                .Include(x => x.Fields.Select(f => f.FieldDefenition))
                .Where(x => x.SchemeId == scheme.Id));

            return new PaginatedEntities
            {
                AllPagesCount = query.GetAllPagesCount(FrontEndDeveloperOptions.Instance.Pagination.Default),
                CurrentPage = pageNumber,
                Entities = await query.OrderByDescending(x => x.PublishDateTime)
                    .Skip((pageNumber - 1) * FrontEndDeveloperOptions.Instance.Pagination.Default)
                    .Take(FrontEndDeveloperOptions.Instance.Pagination.Default)
                    .ToListAsync()
            };
        }

        private void LoadComments(Entity model)
        {
            model.Comments = _comments.Where(c => !c.ParentId.HasValue)
                .Where(c => c.TargetId == model.Id && c.TargetType == CommentTargetType.Entity)
                .OrderBy(c => c.CreateDateTime).ToList();
        }
    }
}