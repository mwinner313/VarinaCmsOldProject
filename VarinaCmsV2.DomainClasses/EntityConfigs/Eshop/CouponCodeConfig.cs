using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;

namespace VarinaCmsV2.DomainClasses.EntityConfigs.Eshop
{
    public class CouponCodeConfig:EntityTypeConfiguration<CouponCode>
    {
        public CouponCodeConfig()
        {
            HasRequired(x => x.Discount).WithMany(x => x.CouponCodes).HasForeignKey(x => x.DiscountId);
            HasOptional(x => x.UsedBy).WithMany().HasForeignKey(x => x.UsedById);
        }
    }
}
