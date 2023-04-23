using System;

namespace MartinDelivery.Api.Models;

public class OrderCourierRequestModel
{
    public int CourierId { get; set; }
    public int OrderId { get; set; }
    public Location CourierLocation { get; set; }
}
