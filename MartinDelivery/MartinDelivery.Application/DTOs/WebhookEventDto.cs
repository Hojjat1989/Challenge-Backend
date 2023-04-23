using System;

namespace MartinDelivery.Application.DTOs;

public class WebhookEventDto
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public string WebhookName { get; set; }
    public string Data { get; set; }
}
