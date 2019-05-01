using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.User.Account;

namespace VarinaCmsV2.ViewModel.DTO
{
    public class UserEntitiesPaginatedLiquidAdapted:LiquidAdapter
    {
        public UserEntitiesPaginatedLiquidAdapted()
        {
            
        }
        public UserLiquidViewModel User { get; set; }
        public List<EntityLiquidAdapter> Entities { get; set; }
        public EntitySchemeLiquidViewModel Scheme { get; set; }
        public int AllPagesCount { get; set; }
        public int CurrentPage { get; set; }

    }
}
