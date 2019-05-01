using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.EntityConfigs
{
    public class TagConfig:EntityTypeConfiguration<Tag>
    {
        public TagConfig()
        {
            HasOptional(x => x.Scheme);
            HasMany(x => x.Pages).WithMany();
            HasRequired(x=>x.Language).WithMany().HasForeignKey(x=>x.LanguageName).WillCascadeOnDelete(false);
        }
    }
}
