using System;
using System.ComponentModel.DataAnnotations.Schema;
using MartinDelivery.Core.Base;

namespace MartinDelivery.Core.Entities;

[Table("OrderLog")]
public class OrderLog:EntityBase
{
    public int OrderId { get; set; }
    public DateTime CreationDate { get; set; }
    public OrderStatus Status { get; set; }
    public int? CourierId { get; set; }
}
