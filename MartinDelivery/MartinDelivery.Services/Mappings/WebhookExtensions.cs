﻿using System;
using MartinDelivery.Application.DTOs;
using MartinDelivery.Core.Entities;

namespace MartinDelivery.Services.Mappings;

public static class WebhookExtensions
{
    public static WebhookEvent ToWebhookEvent(this WebhookEventDto webhookEventDto)
    {
        return new WebhookEvent
        {
            Id = webhookEventDto.Id,
            CreationDate = webhookEventDto.CreationDate,
            Data = webhookEventDto.Data,
            WebhookName = webhookEventDto.WebhookName
        };
    }

    public static WebhookEventDto ToWebhookEventDto(this WebhookEvent webhookEvent)
    {
        return new WebhookEventDto
        {
            Id = webhookEvent.Id,
            CreationDate = webhookEvent.CreationDate,
            Data = webhookEvent.Data,
            WebhookName = webhookEvent.WebhookName
        };
    }

    public static WebhookSubscribe ToWebhookSubscribe(this WebhookSubscribeDto webhookSubscribeDto)
    {
        return new WebhookSubscribe
        {
            Id = webhookSubscribeDto.Id,
            WebhookName = webhookSubscribeDto.WebhookName,
            AuthorizationHeader = webhookSubscribeDto.AuthorizationHeader,
            CreationDate = webhookSubscribeDto.CreationDate,
            OrganizationId = webhookSubscribeDto.OrganizationId,
            Url = webhookSubscribeDto.Url
        };
    }

    public static WebhookSubscribeDto ToWebhookSubscribeDto(this WebhookSubscribe webhookSubscribe)
    {
        return new WebhookSubscribeDto
        {
            Id = webhookSubscribe.Id,
            WebhookName = webhookSubscribe.WebhookName,
            AuthorizationHeader = webhookSubscribe.AuthorizationHeader,
            CreationDate = webhookSubscribe.CreationDate,
            OrganizationId = webhookSubscribe.OrganizationId,
            Url = webhookSubscribe.Url
        };
    }
}
