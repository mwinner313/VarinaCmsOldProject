using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Entities.Enums;

namespace VarinaCmsV2.Core.Services.Category
{
    public class CategoryQuery
    {
        public string Scheme { get; set; }
        [Required]
        public string LanguageName { get; set; }
        public string SearchText { get; set; }
        public CategoryType CategoryType { get; set; }
        public bool CheckByType { get; set; }
        public bool MapForTreeView { get; set; }
    }
}
