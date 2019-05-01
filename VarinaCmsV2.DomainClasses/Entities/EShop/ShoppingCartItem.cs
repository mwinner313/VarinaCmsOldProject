using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.DomainClasses.Entities.EShop
{
    public class ShoppingCartItem:BaseEntity,ITrackibleItem
    {
        public int Quantity { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public User Creator { get; set; }
        public Guid CreatorId { get; set; }
        public virtual Product Product { get; set; }
        public Guid ProductId { get; set; }
        [NotMapped]
        public List<string> Warnings { get; set; }=new List<string>();
    }
}
