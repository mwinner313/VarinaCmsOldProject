using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StructureMap;

namespace VarinaCmsV2.IocCofig
{
    public static class AppObjectFactory
    {
        private static readonly IContainer Container = GetInitalizedContainer();

        public static IContainer GetContainer() => Container;

        internal static IContainer GetInitalizedContainer() => new Container(_ =>
        {
           FindRegistries().ForEach(_.AddRegistry);
            _.Scan(x =>
            {
             
                x.WithDefaultConventions();
            });
            _.For<IContainer>().Singleton().Use(() => Container);
        });

        internal static List<Registry> FindRegistries()
        {

            return Assembly
                    .GetExecutingAssembly()
                    .GetTypes()
                    .Where(x => typeof(Registry).IsAssignableFrom(x))
                    .Select(x => Activator.CreateInstance(x) as Registry)
                    .ToList();
        }
    }
}
