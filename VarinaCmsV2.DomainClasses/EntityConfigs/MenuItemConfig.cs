using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.EntityConfigs
{
    public class MenuItemConfig:EntityTypeConfiguration<MenuItem>
    {
        public MenuItemConfig()
        {
            HasRequired(x => x.Menu).WithMany(x => x.MenuItems).HasForeignKey(x => x.MenuId).WillCascadeOnDelete(true);
            HasMany(x => x.Childs).WithOptional(x => x.Parent).HasForeignKey(x => x.ParentId);
            HasRequired(x => x.Language).WithMany().HasForeignKey(x => x.LanguageName).WillCascadeOnDelete(false);

        }
    }
}
