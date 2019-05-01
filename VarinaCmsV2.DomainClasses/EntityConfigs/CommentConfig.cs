using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.EntityConfigs
{
    public class CommentConfig:EntityTypeConfiguration<Comment>
    {
        public CommentConfig()
        {
            HasMany(x=>x.Childs).WithOptional(x=>x.Parent).HasForeignKey(x=>x.ParentId).WillCascadeOnDelete(false);
        }
    }
}
