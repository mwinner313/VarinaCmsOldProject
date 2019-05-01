using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Modules
{
    public interface  ICmsModuleProvider
    {
        void InitializeInstalledModules();
    }
}
