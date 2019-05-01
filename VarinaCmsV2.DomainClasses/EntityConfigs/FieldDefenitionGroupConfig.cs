using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.EntityConfigs
{
    public class FieldDefenitionGroupConfig:EntityTypeConfiguration<FieldDefenitionGroup>
    {
        public FieldDefenitionGroupConfig()
        {
            HasMany(x=>x.FieldDefenitions).WithOptional(x=>x.FieldDefenitionGroup).HasForeignKey(x=>x.FieldDefenitionGroupId).WillCascadeOnDelete(false);
            HasOptional(x=>x.Scheme).WithMany().HasForeignKey(x=>x.SchemeId).WillCascadeOnDelete(true);
            HasOptional(x=>x.Category).WithMany().HasForeignKey(x=>x.CategoryId).WillCascadeOnDelete(true);
            HasOptional(x=>x.ProductCategory).WithMany().HasForeignKey(x=>x.CategoryId).WillCascadeOnDelete(true);
        }
    }
}
