using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Core.EntityStates
{
    public class EntitySchemeState
    {
        public bool CanDelete => EntitiesCount == 0;
        public long EntitiesCount { get; set; }
    }
}
