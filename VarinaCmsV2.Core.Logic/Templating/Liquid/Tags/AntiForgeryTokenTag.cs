using DotLiquid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Helpers;

namespace VarinaCmsV2.Core.Logic.Templating.Liquid.Tags
{
    public class AntiForgeryTokenTag : Tag
    {
        public override void Render(Context context, TextWriter result)
        {
            result.Write(AntiForgery.GetHtml().ToHtmlString());
        }
    }
}
