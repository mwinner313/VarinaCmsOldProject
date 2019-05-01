using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public static class AutomapperConfiguration
    {
        internal static IContainer Resolver { get; private set; }
        public static void Cofigure(IContainer resolver)
        {

            Resolver = resolver;

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.ConstructServicesUsing(resolver.GetInstance);
                cfg.AddProfiles(typeof(AutomapperConfiguration).Assembly);
            });
        }
    }
}
