using System.Data.Entity.ModelConfiguration;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.DomainClasses.EntityConfigs.Eshop
{
    public class ProductCategoryConfig : EntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryConfig()
        {
            HasMany(x => x.AppliedDiscounts).WithMany(x=>x.AppliedToCategories).Map(x => x.ToTable("ProductCategoryAppliedDiscounts"));
            HasOptional(x => x.Scheme)
                .WithMany().HasForeignKey(x=>x.SchemeId);
            HasOptional(x => x.Parent).WithMany(x => x.Childs).HasForeignKey(x => x.ParentId);
            HasMany(x => x.FieldDefenitions).WithOptional(x => x.ProductCategory)
                .HasForeignKey(x => x.ProductCategoryId).WillCascadeOnDelete(true);
            HasMany(x => x.FieldDefenitionGroups).WithOptional(x => x.ProductCategory)
                .HasForeignKey(x => x.ProductCategoryId).WillCascadeOnDelete(true);
            Property(x => x.AccessType).IsRequired();
        }
    }
}