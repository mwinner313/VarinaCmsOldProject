using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Entities
{
    public interface IEntityWebClientService
    {
        Task<DomainClasses.Entities.Entity> GetByUrlAndSchemeUrlAsync(string entityUrl, string schemeHandle);
        Task<PaginatedEntities> GetByTagAsync(Tag tag,  int pageNumber);
        Task<PaginatedEntities> GetByCategoryAsync(Category category,  int pageNumber);
        Task<UserEntitiesPaginated> GetByUserAsync(User user, string schemeUrl ,int pageNumber);
        Task<DomainClasses.Entities.Entity> CountUpLike(Guid id, IPrincipal user);
        Task<DomainClasses.Entities.Entity> CountDownLike(Guid id, IPrincipal user);
        Task<List<DomainClasses.Entities.Entity>> SearchAsync(string schemeUrl, string searchText);
        Task<PaginatedEntities> GetBySchemeAsync(EntityScheme scheme, int pageNumber);
    }
}
