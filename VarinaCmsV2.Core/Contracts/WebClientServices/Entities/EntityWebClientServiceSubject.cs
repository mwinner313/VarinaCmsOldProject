using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Entities
{
    public class EntityWebClientServiceSubject : IEntityWebClientService
    {
        private readonly List<EntityWebClientServiceObserver> _observers;
        private readonly IEntityWebClientService _service;
        public EntityWebClientServiceSubject(IEntityWebClientService service, List<EntityWebClientServiceObserver> observers)
        {
            _service = service;
            _observers = observers;
        }

        public Task<Entity> CountDownLike(Guid id, IPrincipal user)
        {
            return _service.CountDownLike(id, user);
        }

        public Task<List<Entity>> SearchAsync(string schemeUrl, string searchText)
        {
            return _service.SearchAsync(schemeUrl, searchText);
        }

        public Task<PaginatedEntities> GetBySchemeAsync(EntityScheme scheme, int pageNumber)
        {
            return _service.GetBySchemeAsync(scheme, pageNumber);
        }

        public Task<Entity> CountUpLike(Guid id, IPrincipal user)
        {
            return _service.CountUpLike(id, user);
        }

        public Task<PaginatedEntities> GetByCategoryAsync(Category category, int pageNumber)
        {
            return _service.GetByCategoryAsync(category, pageNumber);
        }

        public Task<PaginatedEntities> GetByTagAsync(Tag tag,  int pageNumber)
        {
            return _service.GetByTagAsync(tag, pageNumber);
        }

        public async Task<Entity> GetByUrlAndSchemeUrlAsync(string entityUrl, string schemeHandle)
        {
            var entity = await _service.GetByUrlAndSchemeUrlAsync(entityUrl, schemeHandle);
            await _observers.ForEachAsync(x => x.EntitySeenByUser(entity));
            return entity;
        }

        public Task<UserEntitiesPaginated> GetByUserAsync(User user, string schemeUrl, int pageNumber)
        {
            return _service.GetByUserAsync(user, schemeUrl, pageNumber);
        }
    }
}
