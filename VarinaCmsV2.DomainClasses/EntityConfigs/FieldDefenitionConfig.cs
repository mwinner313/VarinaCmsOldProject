using System.Data.Entity.ModelConfiguration;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.EntityConfigs
{
    public class FieldDefenitionConfig : EntityTypeConfiguration<FieldDefenition>
    {
        public FieldDefenitionConfig()
        {

            Property(x => x.Type).IsRequired();
            Property(x => x.Handle).IsRequired();
            Property(x => x.Order).IsRequired();
            Property(x => x.Title).IsRequired();
            HasOptional(x => x.ProductCategory).WithMany(x => x.FieldDefenitions)
                .HasForeignKey(x => x.ProductCategoryId);
            HasOptional(x => x.Scheme)
                .WithMany(x => x.FieldDefenitions)
                .HasForeignKey(x => x.SchemeId)
                .WillCascadeOnDelete(true);
            HasOptional(x => x.Category)
                .WithMany(x => x.FieldDefenitions)
                .HasForeignKey(x => x.CategoryId)
                .WillCascadeOnDelete(true);
            HasOptional(x => x.ProductCategory)
                .WithMany(x => x.FieldDefenitions)
                .HasForeignKey(x => x.ProductCategoryId)
                .WillCascadeOnDelete(true);
            HasOptional(x => x.FormScheme)
                .WithMany(x => x.FieldDefenitions)
                .HasForeignKey(x => x.FormSchemeId)
                .WillCascadeOnDelete(true);
          
            HasMany(x=>x.Fields).WithRequired(x=>x.FieldDefenition).HasForeignKey(x=>x.FieldDefenitionId).WillCascadeOnDelete(false);
            HasOptional(x=>x.FieldDefenitionGroup).WithMany(x=>x.FieldDefenitions).HasForeignKey(x=>x.FieldDefenitionGroupId).WillCascadeOnDelete(false);
        }
    }
}