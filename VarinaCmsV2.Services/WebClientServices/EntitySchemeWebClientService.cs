using System.Data.Entity;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.WebClientServices;
using VarinaCmsV2.Core.Contracts.WebClientServices.Entities;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Services.WebClientServices
{
    public class EntitySchemeWebClientService: IEntitySchemeWebClientService
    {
        private readonly IDbSet<EntityScheme> _schemes;
        public EntitySchemeWebClientService(IUnitOfWork unitOfWork)
        {
            _schemes = unitOfWork.Set<EntityScheme>();
        }

        public Task<EntityScheme> GetByUrlAsync(string url)
        {
            return _schemes.FirstOrDefaultAsync(x => x.Url == url);
        }
    }
}
