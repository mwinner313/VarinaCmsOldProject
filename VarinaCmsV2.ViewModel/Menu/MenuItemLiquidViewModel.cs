using System;
using System.Collections.Generic;
using System.Linq;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.Meta;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.ViewModel.Menu
{
    public class MenuItemLiquidViewModel : LiquidAdapter, IHaveChild<MenuItemLiquidViewModel>, IMenuItemMapped
    {
        public string Title { get; set; }
        public int Order { get; set; }
        public string Url { get; set; }
        public Guid Id { get; set; }
        public string TargetType { get; set; }
        public Guid? TargetId { get; set; }
        public string ImagePath { get; set; }
        public string LinkTarget { get; set; }
        public List<MetaDataLiquidViewMdoel> Metas { get; set; }
        public List<MenuItemLiquidViewModel> Childs { get; set; }

        public override object BeforeMethod(string method)
        {
            var metas = Metas.Where(x => x.MetaName == method).ToList();
            if (metas.Count == 0) 
            return base.BeforeMethod(method);

            if (metas.Count == 1) return metas.First();

            return metas;
        }
    }
}
