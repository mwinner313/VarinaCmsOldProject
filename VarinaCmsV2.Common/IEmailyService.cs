using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Common
{
    public interface IEmailyService
    {
        Task SendAsync(EmailContext context);
    }
}
