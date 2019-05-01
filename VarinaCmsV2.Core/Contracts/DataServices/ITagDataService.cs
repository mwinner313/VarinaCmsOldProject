using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Contracts.DataServices
{
    public interface ITagDataService:IDataService<Tag,Guid>
    {
        Task<Tag> GetByUrlAndSchemeUrlAsync(string tagUrl,string schemeUrl);
        Task<Tag> GetByUrlAsync(string tagUrl);
    }



}
