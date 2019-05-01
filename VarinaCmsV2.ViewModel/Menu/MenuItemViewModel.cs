using System;
using System.Collections.Generic;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.ViewModel.Meta;

namespace VarinaCmsV2.ViewModel.Menu
{
    public class MenuItemViewModel:BaseVeiwModel, IHaveChild<MenuItemViewModel>,IMenuItemMapped
    {
        public string Title { get; set; }
        public int Order { get; set; }
        public string Url { get; set; }
        public string LanguageName { get; set; }
        public List<MetaDataViewModel> MetaData { get; set; }
        public string ImagePath { get; set; }
        public string LinkTarget { get; set; }
        public MenuItemTargetType TargetType { get; set; }
        public List<MenuItemViewModel> Childs { get; set; }
        public Guid MenuId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? TargetId { get; set; }
    }
}