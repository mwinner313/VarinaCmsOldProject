using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.DomainClasses.EntityConfigs
{
    public class RoleConfig:EntityTypeConfiguration<Role>
    {
        public RoleConfig()
        {


            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
