using System.Data.Entity;
using System.Linq;

namespace VarinaCmsV2.Common
{
    public abstract class GlobalFilter
    {
       public abstract void ApplyFilter(DbModelBuilder modelBuilder);
    }
}
