using System;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.Core.Services.Form
{
    public class FormQuery:Pagenation
    {
        public string SearchText { get; set; }
        public string FormSchemeHandle { get; set; }
        public bool NotSeen { get; set; }
    }
}