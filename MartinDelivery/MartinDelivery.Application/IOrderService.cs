﻿using System;
using MartinDelivery.Application.DTOs;

namespace MartinDelivery.Application;

public interface IOrderService
{
    int Add(OrderDto order);
    OrderDto GetOrderById(int id);
    OrderListDto GetAvailableOrders(int offset, int size);
    List<OrderDto> GetCourierOrders(int courierId);

    GenericResponse AcceptOrder(int orderId, int courierId);
    GenericResponse ReceiveOrder(int orderId);
    GenericResponse DeliverOrder(int orderId);
    GenericResponse CancelOrder(int orderId);
}
