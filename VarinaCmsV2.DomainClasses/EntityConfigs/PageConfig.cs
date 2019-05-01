using System.Data.Entity.ModelConfiguration;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.EntityConfigs
{
    public class PageConfig:EntityTypeConfiguration<Page>
    {
        public PageConfig()
        {
            
            HasRequired(x => x.Language).WithMany().HasForeignKey(x => x.LanguageName).WillCascadeOnDelete(false);
            HasRequired(x => x.Creator).WithMany().HasForeignKey(x => x.CreatorId);
            Property(x => x.Title).HasMaxLength(80);
            Property(x => x.Title).IsRequired();
            Property(x => x.Url).IsRequired();
            Property(x => x.UrlSegment).IsRequired();
            Property(x => x.Description).IsOptional();
            Property(x => x.AccessType).IsRequired();
            HasOptional(x=>x.Parent).WithMany(x=>x.Childs)
                .HasForeignKey(x=>x.ParentId)
                .WillCascadeOnDelete(false);
            HasMany(x => x.Tags).WithMany().Map(c=>c.ToTable("PagesTags"));
        }
    }
}
