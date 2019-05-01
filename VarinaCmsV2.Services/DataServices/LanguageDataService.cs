using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;
using Z.EntityFramework.Plus;

namespace VarinaCmsV2.Services.DataServices
{
    public class LanguageDataService : ILanguageDataService
    {
        protected  string[] CacheTags => new string[] {FieldDefenitionDataService. FieldDefenitionTag, EntitySchemeDataService.EntityCacheTag };

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Language> _dbSet;
        public LanguageDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<Language>();
        }

        public async Task AddAsync(Language model)
        {
            _dbSet.Add(model);
            await _unitOfWork.SaveChangesAsync();
            QueryCacheManager.ExpireTag(CacheTags);
        }

        public async Task UpdateAsync(Language model)
        {
            _unitOfWork.Update(model);
            await _unitOfWork.SaveChangesAsync();
            QueryCacheManager.ExpireTag(CacheTags);
        }

        public async Task DeleteAsync(string id)
        {
            await _dbSet.Where(x => x.Name == id).DeleteAsync();
            QueryCacheManager.ExpireTag(CacheTags);

        }

        public Task DeleteAsync(List<string> ids)
        {
            throw new NotImplementedException();
        }

        public Task<Language> GetAsync(string id)
        {
            return _dbSet.Where(x => x.Name == id).FirstAsync();
        }

        public IQueryable<Language> Query => _dbSet;
        public Task<IEnumerable<Language>> FromCacheAsync()
        {
            return _dbSet.FromCacheAsync(CacheTags);
        }

        private Language Map(LanguageViewModel model)
        {
            return Mapper.Map<Language>(model);
        }
        private Language Map(LanguageViewModel model, Language entity)
        {
            return Mapper.Map(model, entity);
        }

        public Task<TProjection> GetProjectToAsync<TProjection>(string id)
        {
            return _dbSet.Where(x => x.Name == id).ProjectTo<TProjection>().FirstOrDefaultAsync();
        }

        public void Dispose()
        {
           // _unitOfWork?.Dispose();
        }

        public void Attach(Language model)
        {
            _dbSet.Attach(model);
        }

        public IEnumerable<Language> FromCache()
        {
            return _dbSet.FromCache();
        }
    }
}
