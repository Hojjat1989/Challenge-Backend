using System;
using MartinDelivery.Core.Base;

namespace MartinDelivery.Core.Entities;

public class WebhookSubscribe : EntityBase
{
    public DateTime CreationDate { get; set; }
    public string WebhookName { get; set; }
    public string Url { get; set; }
    public int OrganizationId { get; set; }
    public string AuthorizationHeader { get; set; }
}
