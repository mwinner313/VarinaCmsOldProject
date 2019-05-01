using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap;

namespace VarinaCmsV2.Web.DependencyResolvers
{
    public class MvcDependencyResolver: IDependencyResolver
    {
        private readonly IContainer _container;

        public MvcDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsAbstract || serviceType.IsInterface || !serviceType.IsClass)
                return _container.TryGetInstance(serviceType);
            return _container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}