using System;

namespace MartinDelivery.Api.Webhooks;

public interface IWebhookPublisher
{
    void OrderStatusChanged(int orderId);
}
