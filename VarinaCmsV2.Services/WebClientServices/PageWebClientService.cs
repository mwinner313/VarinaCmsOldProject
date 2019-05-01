using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Contracts.WebClientServices.Pages;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.Enums;

namespace VarinaCmsV2.Services.WebClientServices
{
    public class PageWebClientService : IPageWebClientService
    {
        private readonly IRestrictedItemAccessManager _accessManager;
        private readonly IDbSet<Page> _pages;
        private readonly IDbSet<Comment> _comments;

        public PageWebClientService(IRestrictedItemAccessManager accessManager, IUnitOfWork unitOfWork)
        {
            _accessManager = accessManager;
            _pages = unitOfWork.Set<Page>();
            _comments = unitOfWork.Set<Comment>();
        }

        public async Task<Page> GetByUrl(string pageUrl)
        {
            var model = await _pages.Include(x => x.Tags)
                .Include(x => x.Creator)
                .Include(x => x.Comments)
                .Include(x => x.Tags).Select(x => new
                {
                    Page = x,
                    Comments = _comments.Where(c=>!c.ParentId.HasValue).Where(c => c.TargetType == CommentTargetType.Page && c.TargetId == x.Id)
                    .OrderBy(c=>c.CreateDateTime).ToList()
                })
                .FirstOrDefaultAsync(x => x.Page.Url == pageUrl);
            model.Page.Comments = model.Comments;
            return model.Page;
        }

        public Task<List<Page>> GetByTagAsync(string tagUrl)
        {
            return _accessManager.Filter(_pages.Include(x => x.Tags)
                .Include(x => x.Creator)
                .Include(x => x.Comments)
                .Include(x => x.Tags)
                .Where(x => x.Tags.Any(t => t.Url == tagUrl))
            ).ToListAsync();
        }
    }
}
