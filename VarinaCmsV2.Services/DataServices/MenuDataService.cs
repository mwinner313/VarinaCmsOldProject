using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Services.DataServices
{
    public class MenuDataService:BaseDataService<Menu>,IMenuDataService
    {
        public MenuDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override Task<Menu> GetAsync(Guid id)
        {
            return Query.Include(x => x.MenuItems).FirstAsync(x => x.Id == id);
        }
    }
}
