using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;

namespace VarinaCmsV2.ViewModel.Page
{
    public class PageAddUpdateViewModel:BaseVeiwModel
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public string Handle { get; set; }
        public string Url { get; set; }
        public string UrlSegment { get; set; }
        public Guid? ParentId { get; set; }
        public List<TagViewModel> Tags { get; set; }
        [Required]
        public string LanguageName { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public string PublishDateTime { get; set; }
    }
}
