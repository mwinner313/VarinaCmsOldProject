using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.EntityConfigs
{
    public class EntityShcemeConfig:EntityTypeConfiguration<EntityScheme>
    {
        public EntityShcemeConfig()
        {
            Property(x => x.Url).IsRequired();
            HasMany(x => x.FieldDefenitions).WithOptional(x=>x.Scheme).HasForeignKey(x=>x.SchemeId).WillCascadeOnDelete(true);
            HasMany(x => x.FieldDefenitionGroups).WithOptional(x=>x.Scheme).HasForeignKey(x=>x.SchemeId).WillCascadeOnDelete(true);
            HasMany(x => x.Entities).WithRequired(x => x.Scheme).WillCascadeOnDelete(false);
            Property(x => x.Url).HasColumnAnnotation("Index",
            new IndexAnnotation(new IndexAttribute("IX_Url") { IsUnique = true })).HasMaxLength(40);
        }
    }
}
