using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.EntityConfigs
{
    public class MenuConfig:EntityTypeConfiguration<Menu>
    {
        public MenuConfig()
        {
            HasMany(x => x.MenuItems).WithRequired(x => x.Menu).HasForeignKey(x => x.MenuId).WillCascadeOnDelete(true);
            HasRequired(x => x.Language).WithMany().HasForeignKey(x => x.LanguageName).WillCascadeOnDelete(false);
        }
    }
}
