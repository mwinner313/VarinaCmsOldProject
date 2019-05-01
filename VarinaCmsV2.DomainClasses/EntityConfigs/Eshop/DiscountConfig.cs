using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;

namespace VarinaCmsV2.DomainClasses.EntityConfigs.Eshop
{
    public class DiscountConfig:EntityTypeConfiguration<Discount>
    {
        public DiscountConfig()
        {
            HasMany(x => x.AppliedToProducts).WithMany(x=>x.AppliedDiscounts);
            HasMany(x => x.AppliedToCategories).WithMany();
        }
    }
}
