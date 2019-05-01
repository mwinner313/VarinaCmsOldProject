using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Security.Premissions
{
    public class WidgetPremission:IPremissionList
    {
        public const string CanSee = "widget.see";
        public const string CanAdd = "widget.add";
        public const string CanDelete = "widget.delete";
        public const string CanEdit = "widget.edit";
        public List<string> List { get; }=new List<string>()
        {
            CanSee, CanAdd,CanDelete,CanEdit
        };
    }
}
