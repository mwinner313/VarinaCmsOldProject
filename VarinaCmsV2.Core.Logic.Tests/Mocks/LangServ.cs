using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Logic.Tests.Mocks
{
    public class LangServ:ILanguageDataService
    {
        public  Task AddAsync(Language model)
        {
          return Task.FromResult(0);
        }

        public Task UpdateAsync(Language model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(List<string> ids)
        {
            throw new NotImplementedException();
        }

        public Task<Language> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Language> Query { get; }
        public async Task<IEnumerable<Language>> FromCacheAsync()
        {
           return  new List<Language>()
           {
               new Language() {Name = "en-US"},
               new Language() {Name = "fa-IR"}
           };
        }

        public Task GetProjectToAsync<TProjection>(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TProjection> GetProjectToAsync<TProjection>(string id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public void Attach(Language model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Language> FromCache()
        {
            throw new NotImplementedException();
        }
    }
}
