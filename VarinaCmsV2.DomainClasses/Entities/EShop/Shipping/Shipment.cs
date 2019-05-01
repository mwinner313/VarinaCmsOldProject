using System;
using System.ComponentModel.DataAnnotations;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.DomainClasses.Entities.EShop.Shipping
{
    public class Shipment: IHaveOneToOneRelation
    {
        public Guid Id { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public string TrackingNumber { get; set; }
        public long? TotalWeight { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string AdminComment { get; set; }
//        public Portal Portal { get; set; }
//        public Guid PortalId { get; set; }
    }
}