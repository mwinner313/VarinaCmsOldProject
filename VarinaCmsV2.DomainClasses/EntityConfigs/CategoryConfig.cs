using System.Data.Entity.ModelConfiguration;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.EntityConfigs
{
    public class CategoryConfig:EntityTypeConfiguration<Category>
    {
        public CategoryConfig()
        {
            HasOptional(x => x.Parent).WithMany(x=>x.Childs).HasForeignKey(x=>x.ParentId);
            HasMany(x=>x.FieldDefenitions).WithOptional(x=>x.Category).HasForeignKey(x=>x.CategoryId).WillCascadeOnDelete(true);
            HasMany(x=>x.FieldDefenitionGroups).WithOptional(x=>x.Category).HasForeignKey(x=>x.CategoryId).WillCascadeOnDelete(true);

            Property(x => x.AccessType).IsRequired();
            HasOptional(x => x.Scheme)
                .WithMany()
                .HasForeignKey(x => x.SchemeId);
        }
    }

}
