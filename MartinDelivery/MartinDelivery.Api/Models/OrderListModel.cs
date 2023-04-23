using System;

namespace MartinDelivery.Api.Models;

public class OrderListModel
{
    public OrderModel[] Orders { get; set; }
    public int TotalCount { get; set; }
    public bool HasMore { get; set; }
}
