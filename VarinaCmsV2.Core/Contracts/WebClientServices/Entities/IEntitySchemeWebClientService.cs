using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Entities
{
    public interface IEntitySchemeWebClientService
    {
        Task<EntityScheme> GetByUrlAsync(string url);
    }
}
