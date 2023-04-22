using System;
using MartinDelivery.Core.Base;

namespace MartinDelivery.Core.Entities
{
    public class OrderLog:EntityBase
    {
        public int OrderId { get; set; }
        public DateTime CreationDate { get; set; }
        public OrderStatus Status { get; set; }
        public int? CourierId { get; set; }
    }
}
