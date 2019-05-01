using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.WebClientServices.Comment;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Comment;

namespace VarinaCmsV2.Services.WebClientServices
{
    public class CommentWebClientService : ICommentWebClientService
    {
        private readonly ICommentDataService _commentDataService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<IBaseBeforeAddingEntityLogic> _beforeAdding;
        private readonly List<IBaseBeforeAddingEntityLogic> _afterAdding;
        public CommentWebClientService(ICommentDataService commentDataService, List<IBaseBeforeAddingEntityLogic> beforeAdding, List<IBaseBeforeAddingEntityLogic> afterAdding, IUnitOfWork unitOfWork)
        {
            _commentDataService = commentDataService;
            _beforeAdding = beforeAdding;
            _afterAdding = afterAdding;
            _unitOfWork = unitOfWork;
        }

        public async Task<Comment> AddNewComment(Comment comment)
        {
            _beforeAdding.ForEach(x => x.ApplyLogicAsync(comment, Thread.CurrentPrincipal));
            await _commentDataService.AddAsync(comment);
            _afterAdding.ForEach(x => x.ApplyLogicAsync(comment, Thread.CurrentPrincipal));
            _unitOfWork.Entry(comment).Reference(c=>c.Creator).Load();
            return comment;
        }
    }
}
