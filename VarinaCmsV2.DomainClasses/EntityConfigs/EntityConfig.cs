using System.Data.Entity.ModelConfiguration;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.EntityConfigs
{
    public class EntityConfig:EntityTypeConfiguration<Entity>
    {
        public EntityConfig()
        {
            Map(x =>
            {
                x.MapInheritedProperties();
                x.ToTable("Entities");
            });
            HasMany(x=>x.Fields).WithOptional(x=>x.Entity).HasForeignKey(x=>x.EntityId).WillCascadeOnDelete(true);

            HasRequired(x=>x.Creator).WithMany(x=>x.CreatedEntities).HasForeignKey(x=>x.CreatorId).WillCascadeOnDelete(false);

            HasRequired(x => x.Language).WithMany().HasForeignKey(x => x.LanguageName);

            HasRequired(x=>x.Scheme).WithMany(x=>x.Entities).HasForeignKey(x=>x.SchemeId).WillCascadeOnDelete(false);

            HasMany(x => x.Tags).WithMany();

            HasRequired(x => x.PrimaryCategory).WithMany().HasForeignKey(x => x.PrimaryCategoryId).WillCascadeOnDelete(false);

            HasMany(x => x.RelatedCategories).WithMany().Map(x =>
            {
                x.ToTable("EntityRelatedCategories");
                x.MapLeftKey("EntityId");
                x.MapRightKey("CategoryId");
            });
        }
    }
}
