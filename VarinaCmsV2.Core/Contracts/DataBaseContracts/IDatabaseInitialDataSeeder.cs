using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Data.Contracts;

namespace VarinaCmsV2.Core.Contracts.DataBaseContracts
{
    public interface IDatabaseInitialDataSeeder

    {
        void SeedDataBase(IUnitOfWork unitOfWork); 
    }
}
