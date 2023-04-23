using System;
using Hangfire;
using MartinDelivery.Api.Models;
using MartinDelivery.Api.Utilities;
using MartinDelivery.Application;
using MartinDelivery.Application.DTOs;
using Newtonsoft.Json;

namespace MartinDelivery.Api.Webhooks;

public class WebhookPublisher : IWebhookPublisher
{
    private const string OrderStatusWebhookName = "OrderStatus";

    private IWebhookService _webhookService;
    private IOrderService _orderService;

    public WebhookPublisher(IWebhookService webhookService,
        IOrderService orderService)
    {
        _webhookService = webhookService;
        _orderService = orderService;
    }

    public void OrderStatusChanged(int orderId, Location courierLocation)
    {
        var order = _orderService.GetOrderById(orderId);
        OrderStatusChanged(order, courierLocation);
    }

    public void OrderStatusChanged(OrderDto order, Location courierLocation)
    {
        if (order == null)
        {
            return;
        }

        var orderLocation = new OrderLocationModel
        {
            Order = order.ToOrderModel(),
            CourierLocation = courierLocation
        };
        var serializedOrder = JsonConvert.SerializeObject(orderLocation);
        var webhookEvent = new WebhookEventDto
        {
            CreationDate = DateTime.Now,
            WebhookName = OrderStatusWebhookName,
            Payload = serializedOrder,
        };

        _webhookService.AddWebhookEvent(webhookEvent);
        var eventSubscribers = _webhookService.GetWebhookSubscribers(order.OrganizationId, OrderStatusWebhookName);

        foreach (var item in eventSubscribers)
        {
            // call subscriber
            BackgroundJob.Enqueue<WebhookSender>(sender => sender.Send(item, serializedOrder, 1));
        }
    }
}
