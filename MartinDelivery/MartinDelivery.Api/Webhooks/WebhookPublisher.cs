using System;
using Hangfire;
using MartinDelivery.Application.DTOs;
using MartinDelivery.Application.Interfaces;
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

    public void OrderStatusChanged(int orderId)
    {
        var order = _orderService.GetOrderById(orderId);
        var serializedOrder = JsonConvert.SerializeObject(order);
        var webhookEvent = new WebhookEventDto
        {
            CreationDate = DateTime.Now,
            WebhookName = OrderStatusWebhookName,
            Data = serializedOrder,
        };

        _webhookService.AddWebhookEvent(webhookEvent);
        var eventSubscribers = _webhookService.GetWebhookSubscribers(order.OrganizationId, OrderStatusWebhookName);

        foreach (var item in eventSubscribers)
        {
            // call subscriber
            BackgroundJob.Enqueue<WebhookSender>(sender => sender.Send(item, webhookEvent.Data, 1));
        }
    }
}
