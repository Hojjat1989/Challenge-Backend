using System;
using System.ComponentModel.DataAnnotations.Schema;
using MartinDelivery.Core.Base;

namespace MartinDelivery.Core.Entities;

[Table("WebhookAttempt")]
public class WebhookAttempt : EntityBase
{
    public DateTime CreationDate { get; set; }
    public string WebhookName { get; set; }
    public string Payload { get; set; }
    public string Url { get; set; }
    public string AuthorizationHeader { get; set; }
    public int OrganizationId { get; set; }
    public string ResponseStatusCode { get; set; }
}
