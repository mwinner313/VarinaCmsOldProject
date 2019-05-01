using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Services.EntityScheme
{
    public class EntitySchemeQuery:Pagenation,IServiceRequestQuery
    {
        public string LanguageName { get; set; }
        public SchemeType Type { get; set; }
    }
}
