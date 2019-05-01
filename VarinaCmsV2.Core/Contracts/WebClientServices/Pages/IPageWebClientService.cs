using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Pages
{
    public interface IPageWebClientService
    {
        Task<Page> GetByUrl(string pageUrl);
        Task<List<Page>> GetByTagAsync(string tagUrl);
    }
}
