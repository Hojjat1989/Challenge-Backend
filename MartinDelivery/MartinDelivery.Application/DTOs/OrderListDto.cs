using System;

namespace MartinDelivery.Application.DTOs;

public class OrderListDto
{
    public OrderDto[] Orders { get; set; }
    public int TotalCount { get; set; }
    public bool HasMore { get; set; }
}
