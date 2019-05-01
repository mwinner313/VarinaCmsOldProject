using System.Data.Entity.ModelConfiguration;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.EntityConfigs
{
    public class FieldConfig:EntityTypeConfiguration<Field>
    {
        public FieldConfig()
        {
            HasRequired(x => x.FieldDefenition).WithMany().HasForeignKey(x=>x.FieldDefenitionId).WillCascadeOnDelete(false);
            HasOptional(x=>x.Entity).WithMany(x=>x.Fields).HasForeignKey(x=>x.EntityId).WillCascadeOnDelete(true);
        }
    }
}
