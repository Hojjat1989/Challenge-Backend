﻿using System;

namespace MartinDelivery.Api.Models;

public class CreateOrderModel
{
    public int OrganizationId { get; set; }
    public string SenderName { get; set; }
    public string SenderPhone { get; set; }
    public string SenderAddress { get; set; }
    public double SenderLatitude { get; set; }
    public double SenderLongitude { get; set; }

    public string ReceiverName { get; set; }
    public string ReceiverPhone { get; set; }
    public string ReceiverAddress { get; set; }
    public double ReceiverLatitude { get; set; }
    public double ReceiverLongitude { get; set; }
}
