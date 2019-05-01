using System.Data.Entity.ModelConfiguration;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.DomainClasses.EntityConfigs.Eshop
{
    public class ProductConfig : EntityTypeConfiguration<Product>
    {
        public ProductConfig()
        {
            HasRequired(x => x.PrimaryCategory).WithMany(x => x.Products).HasForeignKey(x => x.PrimaryCategoryId).WillCascadeOnDelete(false);

            HasMany(x => x.RelatedCategories).WithMany().Map(x =>
                x.ToTable("ProductRelatedCategories").MapLeftKey("ProductId").MapRightKey("CategoryId"));

            HasRequired(x => x.Creator).WithMany().HasForeignKey(x => x.CreatorId).WillCascadeOnDelete(false);

            HasMany(x => x.UpSellings).WithMany().Map(x => x.ToTable("ProductUpSelligs"));
            HasMany(x => x.Fields).WithOptional().HasForeignKey(x => x.ProductId).WillCascadeOnDelete(true);
            HasMany(x => x.CrossSellings).WithMany().Map(x =>
            {
                x.ToTable("ProductCrossSellings");
                x.MapLeftKey("ProductId");
                x.MapRightKey("CrossSelledId");
            });

            HasMany(x => x.RequiredProducts).WithMany().Map(x =>
            {
                x.ToTable("RequiredProducts");
                x.MapLeftKey("ProductId");
                x.MapRightKey("RequiredProductId");
            });

            HasMany(x => x.Eshantions).WithMany().Map(x =>
            {
                x.ToTable("ProductEshantions");
                x.MapLeftKey("ProductId");
                x.MapRightKey("EshantionId");
            });

            HasMany(x => x.Pictures).WithRequired().HasForeignKey(x=>x.ProductId).WillCascadeOnDelete(true);

            HasMany(x => x.AppliedDiscounts).WithMany();

            HasMany(x => x.ProductReviews).WithRequired(x => x.Product).HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(true);

            HasMany(x => x.Tags).WithMany()
                .Map(x => x.ToTable("ProductTags").MapLeftKey("ProductId").MapRightKey("TagId"));

            HasMany(x => x.AssociatedProducts).WithOptional(x => x.ParentGrouped)
                .HasForeignKey(x => x.ParentGroupedProductId);

            HasMany(x => x.AppliedDiscounts).WithMany().Map(x => x.ToTable("ProductApplieedDiscounts"));

            HasMany(x => x.DeliveryDates).WithMany();
            HasMany(x => x.OrderItems).WithRequired(x => x.Product).HasForeignKey(x => x.ProductId);
        }
    }
}