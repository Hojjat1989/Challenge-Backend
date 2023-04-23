using System;
using Hangfire;
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

    public void OrderStatusChanged(int orderId)
    {
        var order = _orderService.GetOrderById(orderId);
        if (order == null)
        {
            return;
        }

        var serializedOrder = JsonConvert.SerializeObject(order.ToOrderModel());
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
            BackgroundJob.Enqueue<WebhookSender>(sender => sender.Send(item, webhookEvent.Payload, 1));
        }
    }
}
