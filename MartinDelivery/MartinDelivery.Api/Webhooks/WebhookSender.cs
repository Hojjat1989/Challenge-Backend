using System;
using System.Net;
using Hangfire;
using MartinDelivery.Application.DTOs;
using RestSharp;

namespace MartinDelivery.Api.Webhooks;

public class WebhookSender
{
    private const string AuthHeaderName = "authorization";
    private const int MaxAttemps = 5;
    private const int AttemptsDelay = 20;

    public void Send(WebhookSubscribeDto subscribe, string data, int attempt)
    {
        var request = new RestRequest(subscribe.Url, Method.Post);
        request.AddHeader(AuthHeaderName, subscribe.AuthorizationHeader);
        request.AddJsonBody(data);
        var client = new RestClient();
        var response = client.Execute(request);

        if (response.StatusCode == HttpStatusCode.OK || attempt == MaxAttemps)
        {
            return;
        }

        BackgroundJob.Schedule<WebhookSender>(
            sender => sender.Send(subscribe, data, attempt + 1),
            TimeSpan.FromSeconds(attempt * AttemptsDelay));
    }
}
