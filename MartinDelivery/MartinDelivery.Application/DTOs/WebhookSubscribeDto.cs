using System;

namespace MartinDelivery.Application.DTOs;

public class WebhookSubscribeDto
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public int OrganizationId { get; set; }
    public string WebhookName { get; set; }
    public string Url { get; set; }
    public string AuthorizationHeader { get; set; }
}
