using System.Threading.Tasks;
using VarinaCmsV2.ViewModel.Comment;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Comment
{
    public interface ICommentWebClientObserver
    {
        Task CommentAdded(DomainClasses.Entities.Comment comment);
    }
}