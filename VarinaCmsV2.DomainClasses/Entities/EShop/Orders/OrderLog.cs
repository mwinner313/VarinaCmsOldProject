using System;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.DomainClasses.Entities.EShop.Orders
{
    public class OrderLog : BaseEntity
    {
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public User Creator { get; set; }
        public Guid CreatorId { get; set; }
        public string Log { get; set; }
        public Order Order { get; set; }
        public Guid OrderId { get; set; }
    }
}