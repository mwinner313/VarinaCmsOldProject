using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using StructureMap;

namespace VarinaCmsV2.IocCofig
{
    internal static class TypeHelper
    {
        internal static List<T> FindListOf<T>(StructureMap.IContext c=null) where T : class
        {
            var baseType = typeof(T);
            var appAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(x=>x.Name.StartsWith("Varina")).Select(Assembly.Load).ToList();
            var allFoundType = new List<T>();
            foreach (var ass in appAssemblies)
            {
                var foundTypes = ass.GetTypes().Where(x => baseType.IsAssignableFrom(x) && baseType != x &&!x.IsAbstract).ToList();
                 var objs= foundTypes.Select(Activator.CreateInstance)
                     .Select(x => x as T)
                     .ToList();
                allFoundType.AddRange(objs);
            }
            if(c!=null)
            allFoundType.ForEach(c.BuildUp);

            return allFoundType;
        }

        internal static List<T> FindListOfByContainer<T>(StructureMap.IContext c ) where T : class
        {
            var baseType = typeof(T);
            var appAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(x => x.Name.StartsWith("Varina")).Select(Assembly.Load).ToList();
            var allFoundType = new List<T>();
            foreach (var ass in appAssemblies)
            {
                var foundTypes = ass.GetTypes().Where(x => baseType.IsAssignableFrom(x) && baseType != x && !x.IsAbstract).ToList();
                var objs = foundTypes.Select(c.GetInstance)
                    .Select(x => x as T)
                    .ToList();
                allFoundType.AddRange(objs);
            }
            return allFoundType;
        }

    }
}
