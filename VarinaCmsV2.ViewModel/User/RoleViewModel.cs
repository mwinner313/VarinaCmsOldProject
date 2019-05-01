using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.ViewModel.User
{
    public class RoleViewModel:BaseVeiwModel
    {
        public string Name { get; set; }
        public List<Premission> Premissions { get; set; }
    }
}
