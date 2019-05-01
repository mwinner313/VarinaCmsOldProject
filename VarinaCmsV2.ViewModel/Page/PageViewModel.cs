using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.ViewModel.Page
{
    public class PageViewModel:BaseVeiwModel, IUrlableViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public string Handle { get; set; }
        public string UrlSegment { get; set; }
        public Guid? ParentId { get; set; }
        public string ParentTitle { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public string LanguageName { get; set; }
        public bool IsDeleted { get; set; }
        public string PublishDateTime { get; set; }
        public string UpdateDateTime { get; set; } 
        public string CreateDateTime { get; set; } 
        public string CreatorUserName { get; set; }
        public string ToUrl { get; set; }
        public string ToFullUrl { get; set; }
    }
}
