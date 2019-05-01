using System.Data.Entity.ModelConfiguration;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.EntityConfigs
{
    public class LanguageConfig:EntityTypeConfiguration<Language>
    {
        public LanguageConfig()
        {
            Property(x => x.ResourceAddress).IsRequired();
            HasKey(x => x.Name);
        }
    }
}
