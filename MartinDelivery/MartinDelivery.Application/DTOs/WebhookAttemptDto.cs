using System;

namespace MartinDelivery.Application.DTOs;

public class WebhookAttemptDto
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public string WebhookName { get; set; }
    public string Payload { get; set; }
    public int OrganizationId { get; set; }
    public string ResponseStatusCode { get; set; }
}
