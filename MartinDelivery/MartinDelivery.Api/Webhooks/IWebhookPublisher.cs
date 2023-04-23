using System;
using MartinDelivery.Api.Models;
using MartinDelivery.Application.DTOs;

namespace MartinDelivery.Api.Webhooks;

public interface IWebhookPublisher
{
    void OrderStatusChanged(int orderId, Location courierLocation);
    void OrderStatusChanged(OrderDto order, Location courierLocation);
}
