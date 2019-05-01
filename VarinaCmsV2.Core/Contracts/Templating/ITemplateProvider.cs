using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Contracts.Templating
{
    public interface ITemplateProvider
    {
        string TemplatesBasePath { get; set; }
        string Extension { get; set; }
        string GetTemplateString(ITemplatedItem item);
        string GetHomePageTemplateString();
         //string GetLoginPath();
        string Get404NotFoundTemplateString();
    }
}
