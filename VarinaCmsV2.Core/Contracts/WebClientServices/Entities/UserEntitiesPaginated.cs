using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Entities
{
    public class UserEntitiesPaginated:IUrlable
    {
        public User User { get; set; }
        public List<Entity> Entities { get; set; }
        public EntityScheme Scheme { get; set; }
        public int AllPagesCount { get; set; }
        public int CurrentPage { get; set; }
        public string Url { get; set; }
    }
}
