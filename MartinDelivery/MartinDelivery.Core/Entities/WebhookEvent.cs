using System;
using MartinDelivery.Core.Base;

namespace MartinDelivery.Core.Entities;

public class WebhookEvent : EntityBase
{
    public DateTime CreationDate { get; set; }
    public string WebhookName { get; set; }
    public string Data { get; set; }
}
