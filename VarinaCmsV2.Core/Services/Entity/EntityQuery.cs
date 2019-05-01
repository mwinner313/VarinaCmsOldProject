using System;
using System.Collections.Generic;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.Core.Services.Entity
{
    public class EntityQuery:Pagenation
    {
        public Guid? SchemeId { get; set; }
        public string Scheme { get; set; }
        public string LanguageName { get; set; }
        public Guid PrimaryCategoryId { get; set; }
        public List<Guid> RelatedCategoryIds { get; set; }
        public bool IncludeChildCategories { get; set; }
        public string SearchText { get; set; }
        public List<Guid> TagIds { get; set; }
        public PublishState PublishState { get; set; }
        public bool NotVisibles { get; set; }
        public string Order { get; set; }
    }
}