using System;
using System.Collections.Generic;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.Enums;

namespace VarinaCmsV2.DomainClasses.Entities
{
    public class MenuItem:BaseEntity,IGlobalizedItem,IHaveMetaData
    {
        public const string MetaTypeName = nameof(MenuItem);
        public string Title { get; set; }
        public virtual ICollection<MenuItem> Childs { get; set; }
        public virtual MenuItem Parent { get; set; }
        public Menu Menu { get; set; }
//        public Portal Portal { get; set; }
//        public Guid PortalId { get; set; }
        public Guid MenuId { get; set; }
        public Guid? ParentId { get; set; }
        public int Order { get; set; }
        public string Url { get; set; }
        public string ImagePath { get; set; }
        public string LinkTarget { get; set; }
        public Guid? TargetId { get; set; }
        public MenuItemTargetType TargetType { get; set; }
        public string LanguageName { get; set; }
        public Language Language { get; set; }
    }
}
