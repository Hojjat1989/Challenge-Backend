using System;
using System.Net;
using Hangfire;
using MartinDelivery.Application;
using MartinDelivery.Application.DTOs;
using RestSharp;

namespace MartinDelivery.Api.Webhooks;

public class WebhookSender
{
    private const string AuthHeaderName = "authorization";
    private const int MaxAttemps = 5;
    private const int AttemptsDelay = 20;

    private IWebhookService _webhookService;

    public WebhookSender(IWebhookService webhookService)
    {
        _webhookService = webhookService;
    }

    public void Send(WebhookSubscribeDto subscribe, string payload, int attempt)
    {
        var request = new RestRequest(subscribe.Url, Method.Post);
        request.AddHeader(AuthHeaderName, subscribe.AuthorizationHeader);
        request.AddJsonBody(payload);
        var client = new RestClient();
        var response = client.Execute(request);

        AddWebhookAttempt(subscribe, payload, response.StatusCode);

        if (response.StatusCode == HttpStatusCode.OK || attempt == MaxAttemps)
        {
            return;
        }

        BackgroundJob.Schedule<WebhookSender>(
            sender => sender.Send(subscribe, payload, attempt + 1),
            TimeSpan.FromSeconds(attempt * AttemptsDelay));
    }

    private void AddWebhookAttempt(WebhookSubscribeDto subscribeDto, string payload, HttpStatusCode statusCode)
    {
        var attempt = new WebhookAttemptDto
        {
            CreationDate = DateTime.Now,
            OrganizationId = subscribeDto.OrganizationId,
            Payload = payload,
            WebhookName = subscribeDto.WebhookName,
            ResponseStatusCode = statusCode.ToString()
        };
        _webhookService.AddWebhookAttempt(attempt);
    }
}
