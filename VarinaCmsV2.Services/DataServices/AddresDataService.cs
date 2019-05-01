using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Services.DataServices
{
    public class AddressDataService:BaseDataService<Address>,IAddressDataService
    {
        public AddressDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
