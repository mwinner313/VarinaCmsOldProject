using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Contracts.DataServices
{
    public interface ICategoryDataService:IDataService<Category,Guid>
    {
        Task<Category> GetByUrlAndSchemeUrlAsync(string categoryUrl, string schemeUrl);
    }
}
