using System.Threading.Tasks;
using VarinaCmsV2.ViewModel.Comment;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Comment
{
    public interface ICommentWebClientService
    {
        Task<DomainClasses.Entities.Comment> AddNewComment(DomainClasses.Entities.Comment comment);
    }
}
