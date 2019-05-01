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
    public interface IModule
    {
        void Initialize(NameValueCollection settings, IContainer container);
        void SetUpDataBase();
    }
}
