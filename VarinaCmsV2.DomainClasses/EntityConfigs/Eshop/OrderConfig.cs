using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;

namespace VarinaCmsV2.DomainClasses.EntityConfigs.Eshop
{
    public class OrderConfig:EntityTypeConfiguration<Order>
    {
        public OrderConfig()
        {
            HasOptional(x => x.Shipment).WithRequired().WillCascadeOnDelete(true);
            HasOptional(x => x.ShippingAddress).WithMany().HasForeignKey(x=>x.ShippingAddressId).WillCascadeOnDelete(false);
            HasMany(x=>x.OrderItems).WithRequired(x=>x.Order).HasForeignKey(x=>x.OrderId).WillCascadeOnDelete(false);
            HasMany(x => x.OrderLogs).WithRequired(x => x.Order).HasForeignKey(x => x.OrderId).WillCascadeOnDelete(false);
        }
    }
}
