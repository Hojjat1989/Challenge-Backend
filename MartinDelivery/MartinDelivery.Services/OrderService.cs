using System;
using MartinDelivery.Application.DTOs;
using MartinDelivery.Application.Interfaces;
using MartinDelivery.Core.Base;
using MartinDelivery.Core.Entities;
using MartinDelivery.Services.Mappings;

namespace MartinDelivery.Services;

public class OrderService : IOrderService
{
    private IRepository<Order> _orderRepository;

    public OrderService(IRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public int Add(OrderDto order)
    {
        var orderEntity = order.ToOrder();
        _orderRepository.Add(orderEntity);
        return orderEntity.Id;
    }
}
