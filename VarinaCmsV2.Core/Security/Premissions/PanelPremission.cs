using System.Collections.Generic;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Security.Premissions
{
    public  class PanelPremission: IPremissionList
    {
        public const string Default = "panel.default";
        public List<string> List { get; set; }=new List<string>() {Default};
    }
}
