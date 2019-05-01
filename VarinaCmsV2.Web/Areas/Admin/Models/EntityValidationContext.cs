using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarinaCmsV2.Web.Areas.Admin.Models
{
    public class EntityValidationContext
    {
        public string Value { get; set; }
        public Guid SchemeId { get; set; }
        public Guid? Id { get; set; }
    }
}