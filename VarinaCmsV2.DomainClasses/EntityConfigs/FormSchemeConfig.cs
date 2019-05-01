using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.EntityConfigs
{
    public class FormSchemeConfig:EntityTypeConfiguration<FormScheme>
    {
        public FormSchemeConfig()
        {
            Property(x => x.Title).IsRequired();
            Property(x => x.Handle).IsRequired();
            HasMany(x => x.FieldDefenitions).WithOptional(x => x.FormScheme).HasForeignKey(x => x.FormSchemeId).WillCascadeOnDelete(true);
            HasMany(x => x.Forms).WithRequired(x => x.FormScheme).HasForeignKey(x => x.FormSchemeId).WillCascadeOnDelete(false);
        }
    }
}
