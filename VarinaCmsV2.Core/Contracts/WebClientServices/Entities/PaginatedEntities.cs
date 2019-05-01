using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Entities
{
    public class PaginatedEntities
    {
        public List<Entity> Entities { get; set; }
        public int CurrentPage { get; set; }
        public int AllPagesCount { get; set; }
    }
}