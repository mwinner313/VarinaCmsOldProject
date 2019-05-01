using System.Collections.Generic;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.ViewModel.Comment;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Comment
{
    public class CommentWebClientServiceSubject : ICommentWebClientService
    {
        private ICommentWebClientService _wrappe;
        private readonly List<ICommentWebClientObserver> _observers = new List<ICommentWebClientObserver>();
        public async Task<DomainClasses.Entities.Comment> AddNewComment(DomainClasses.Entities.Comment comment)
        {
            var res = await _wrappe.AddNewComment(comment);
            await _observers.ForEachAsync(x => x.CommentAdded(comment));
            return res;
        }

        public void AddObserver(ICommentWebClientObserver observer)
        {
            _observers.Add(observer);
        }

        public void SetWrappe(ICommentWebClientService wrappe)
        {
            _wrappe = wrappe;
        }
    }
}
