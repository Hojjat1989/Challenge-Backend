using System;
using MartinDelivery.Core.Base;

namespace MartinDelivery.Core.Entities
{
    public class Order : EntityBase
    {
        public int OrganizationId { get; set; }
        public int? CourierId { get; set; }
        public DateTime CreationDate { get; set; }
        public OrderStatus Status { get; set; }
    }
}
