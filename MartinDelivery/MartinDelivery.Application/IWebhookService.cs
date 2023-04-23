using System;
using MartinDelivery.Application.DTOs;

namespace MartinDelivery.Application;

public interface IWebhookService
{
    int AddWebhookEvent(WebhookEventDto webhookEvent);
    int AddWebhookSubscribe(WebhookSubscribeDto webhookSubscribe);
    void AddWebhookAttempt(WebhookAttemptDto webhookAttempt);
    List<WebhookSubscribeDto> GetWebhookSubscribers(int organizationId, string webhookName);
}
