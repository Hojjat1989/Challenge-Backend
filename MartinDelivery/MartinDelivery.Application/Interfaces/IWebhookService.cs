using System;
using MartinDelivery.Application.DTOs;

namespace MartinDelivery.Application.Interfaces;

public interface IWebhookService
{
    int AddWebhookEvent(WebhookEventDto webhookEvent);
    int AddWebhookSubscribe(WebhookSubscribeDto webhookSubscribe);
    List<WebhookSubscribeDto> GetWebhookSubscribers(int organizationId, string webhookName);
}
