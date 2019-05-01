using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.EntityConfigs
{
    public class FormConfig:EntityTypeConfiguration<Form>
    {
        public FormConfig()
        {
            HasOptional(x => x.Creator).WithMany().HasForeignKey(x => x.CreatorId);

            HasMany(x=>x.Fields).WithOptional(x=>x.Form).HasForeignKey(x=>x.FormId).WillCascadeOnDelete(true);
        }
    }
}
