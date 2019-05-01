using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.Widgets;

namespace VarinaCmsV2.DomainClasses.EntityConfigs
{
    public class WidgetConfig:EntityTypeConfiguration<Widget>
    {
        public WidgetConfig()
        {
            HasRequired(x=>x.Container).WithMany(x=>x.Widgets).HasForeignKey(x=>x.ContainerId).WillCascadeOnDelete(false);
            HasRequired(x=>x.Language).WithMany().HasForeignKey(x=>x.LanguageName).WillCascadeOnDelete(false);
        }
    }
}
