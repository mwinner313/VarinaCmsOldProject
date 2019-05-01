using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VarinaCmsV2.Data.Conventions
{
    public class MapProtectedFieldConvention:Convention
    {
        public MapProtectedFieldConvention()
        {
          Types().Configure(c =>
          {
              var nonPublicProperties = c.ClrType.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);

              foreach (var p in nonPublicProperties)
              {
                  c.Property(p).HasColumnName(p.Name);
              }
          });
        }
    }
}
