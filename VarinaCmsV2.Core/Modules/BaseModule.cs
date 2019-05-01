using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;

namespace VarinaCmsV2.Core.Modules
{
    public abstract class BaseModule:IModule
    {
        private readonly IContainer _container;

        protected BaseModule(IContainer container,ProviderSettings settings)
        {
            _container = container;
        }

        public virtual void Initialize(NameValueCollection settings, IContainer container)
        {

        }

        public virtual void SetUpDataBase()
        {

        }
    }
}
