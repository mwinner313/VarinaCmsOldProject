using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.ViewModel.Meta;

namespace VarinaCmsV2.ViewModel.Menu
{
    public class MenuItemAddUpdateViewModel: BaseVeiwModel
    {
        public string Title { get; set; }
        public int Order { get; set; }
        public string Url { get; set; }
        public string ImagePath { get; set; }
        public string LinkTarget { get; set; }
        public string LanguageName { get; set; }
        public List<MetaDataAddOrUpdateViewModel> MetaData { get; set; }
        public MenuItemTargetType TargetType { get; set; }
        public ICollection<MenuItemViewModel> Childs { get; set; }
        public Guid MenuId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? TargetId { get; set; }
    }
}
