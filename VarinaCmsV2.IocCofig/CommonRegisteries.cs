using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.Common.Web;
using VarinaCmsV2.FileManager.Files;
using VarinaCmsV2.Services.CommonServices;

namespace VarinaCmsV2.IocCofig
{
    public class CommonRegisteries:StructureMap.Registry
    {
        public CommonRegisteries()
        {
            For<IUrlBuilder>().Singleton().Use<UrlBuilder>();
            For<IEmailyService>().Singleton().Use<EmailService>();
            For<IFileManager>().Singleton().Use<FileManager.Files.FileManager>();
            For<IUserAgentHelper>().Singleton().Use<UserAgentHelper>();
        }
    }
}
