using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Services.DataServices
{
    public class UserDataService:BaseDataService<User>,IUserDataService
    {
        public UserDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override Task<User> GetAsync(Guid id)
        {
            return  DataSet.Where(x => x.Id == id).Include(x=>x.Roles).FirstOrDefaultAsync();
        }
    }
}
