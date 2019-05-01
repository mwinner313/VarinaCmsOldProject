using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Services.DataServices
{
    public class PageDataService : BaseDataService<Page>, IPageDataService
    {
        public PageDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

      
    }
}
