using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.EntityConfigs
{
    public class FileDataConfig:EntityTypeConfiguration<FileData>
    {
        public FileDataConfig()
        {
           HasOptional(x=>x.Creator)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}
