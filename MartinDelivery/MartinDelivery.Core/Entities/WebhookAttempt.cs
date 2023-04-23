using System;
using MartinDelivery.Core.Base;

namespace MartinDelivery.Core.Entities;

public class WebhookAttempt : EntityBase
{
    public DateTime CreationDate { get; set; }
    public string WebhookName { get; set; }
    public string Payload { get; set; }
    public int OrganizationId { get; set; }
    public string ResponseStatusCode { get; set; }
}
