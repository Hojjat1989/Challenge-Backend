using System;
using MartinDelivery.Application;
using MartinDelivery.Application.DTOs;
using MartinDelivery.Core.Base;
using MartinDelivery.Core.Entities;
using MartinDelivery.Services.Mappings;

namespace MartinDelivery.Services;

public class WebhookService : IWebhookService
{
    private readonly IRepository<WebhookEvent> _webhookEventRepository;
    private readonly IRepository<WebhookSubscribe> _webhookSubscribeRepository;
    private readonly IRepository<WebhookAttempt> _webhookAttemptRepository;

    public WebhookService(IRepository<WebhookEvent> webhookEventRepository,
        IRepository<WebhookSubscribe> webhookSubscribeRepository,
        IRepository<WebhookAttempt> webhookAttemptRepository)
    {
        _webhookEventRepository = webhookEventRepository;
        _webhookSubscribeRepository = webhookSubscribeRepository;
        _webhookAttemptRepository = webhookAttemptRepository;
    }

    public void AddWebhookAttempt(WebhookAttemptDto webhookAttempt)
    {
        var dbWebhookAttempt = webhookAttempt.ToWebhookAttempt();
        _webhookAttemptRepository.Add(dbWebhookAttempt);
    }

    public int AddWebhookEvent(WebhookEventDto webhookEvent)
    {
        var dbWebhook = webhookEvent.ToWebhookEvent();
        _webhookEventRepository.Add(dbWebhook);
        return dbWebhook.Id;
    }

    public int AddWebhookSubscribe(WebhookSubscribeDto webhookSubscribe)
    {
        var dbWebhookSubscribe = webhookSubscribe.ToWebhookSubscribe();
        _webhookSubscribeRepository.Add(dbWebhookSubscribe);
        return dbWebhookSubscribe.Id;
    }

    public List<WebhookSubscribeDto> GetWebhookSubscribers(int organizationId, string webhookName)
    {
        var result = _webhookSubscribeRepository
            .GetAll(x => x.OrganizationId == organizationId && x.WebhookName == webhookName).ToList();

        return result.Select(x => x.ToWebhookSubscribeDto()).ToList();
    }
}
