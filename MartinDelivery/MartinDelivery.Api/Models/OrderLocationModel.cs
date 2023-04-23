using System;

namespace MartinDelivery.Api.Models;

public class OrderLocationModel
{
    public OrderModel Order { get; set; }
    public Location CourierLocation { get; set; }
}
