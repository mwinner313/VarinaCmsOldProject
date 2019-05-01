using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarinaCmsV2.Web.Areas.Admin.Models
{
    public class TagQuery
    {
        public string SearchText { get; set; }
        public Guid? SchemeId { get; set; }
    }
}