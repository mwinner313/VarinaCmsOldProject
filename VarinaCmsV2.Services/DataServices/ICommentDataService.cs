using System;
using System.Data.Entity;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Services.DataServices
{
    public class CommentDataService : BaseDataService<Comment>, ICommentDataService
    {
        public CommentDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override Task<Comment> GetAsync(Guid id)
        {
            return Query.Include(x => x.Creator)
                .Include(x => x.Parent).Include(x => x.Parent.Creator)
                   .FirstAsync(x => x.Id == id);
        }
    }
}
