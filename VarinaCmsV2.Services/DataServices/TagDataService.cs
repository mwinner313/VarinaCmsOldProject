using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataBaseContracts;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Services.DataServices
{
    public class TagDataService:BaseDataService<Tag>,ITagDataService
    {
        public TagDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<Tag> GetByUrlAndSchemeUrlAsync(string tagUrl, string schemeUrl)
        {
            return Query.Include(x=>x.Scheme).FirstAsync(x => x.Scheme.Url == schemeUrl && x.Url == tagUrl);
        }

        public async Task<Tag> GetByUrlAsync(string tagUrl)
        {
            return await Query.FirstAsync(x => x.Url == tagUrl);
        }
    }
}
