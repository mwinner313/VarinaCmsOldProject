using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Entities
{
    public abstract class EntityWebClientServiceObserver
    {
       public abstract Task  EntitySeenByUser(DomainClasses.Entities.Entity entity);
    }
}