﻿using System;
using MartinDelivery.Application.DTOs;

namespace MartinDelivery.Application.Interfaces;

public interface IOrderService
{
    int Add(OrderDto order);

    GenericResponse AcceptOrder(int orderId, int courierId);
    GenericResponse CancelOrder(int orderId);
}
