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
    private IRepository<OrderLog> _orderLogRepository;

    public OrderService(IRepository<Order> orderRepository,
        IRepository<OrderLog> orderLogRepository)
    {
        _orderRepository = orderRepository;
        _orderLogRepository = orderLogRepository;
    }

    public int Add(OrderDto order)
    {
        var orderEntity = order.ToOrder();
        _orderRepository.Add(orderEntity);
        AddOrderLog(orderEntity);

        return orderEntity.Id;
    }

    public OrderDto GetOrderById(int id)
    {
        var order = _orderRepository.GetById(id);
        return order.ToOrderDto();
    }

    public GenericResponse AcceptOrder(int orderId, int courierId)
    {
        var order = _orderRepository.GetById(orderId);
        var generalCheck = CheckOrderStatusChange(order);
        if (!generalCheck.IsSuccessful)
        {
            return generalCheck;
        }

        if (order.Status != OrderStatus.New)
        {
            return new GenericResponse
            {
                IsSuccessful = false,
                Message = "این سفارش در وضعیت درست نیست."
            };
        }

        // this should be done in one commit by implementing unitOfWork pattern
        order.Status = OrderStatus.Accepted;
        order.CourierId = courierId;
        _orderRepository.Update(order);
        AddOrderLog(order);

        return new GenericResponse
        {
            IsSuccessful = true,
            Message = "انجام شد"
        };
    }

    public GenericResponse ReceiveOrder(int orderId)
    {
        var order = _orderRepository.GetById(orderId);
        var generalCheck = CheckOrderStatusChange(order);
        if (!generalCheck.IsSuccessful)
        {
            return generalCheck;
        }

        if (order.Status != OrderStatus.Accepted)
        {
            return new GenericResponse
            {
                IsSuccessful = false,
                Message = "این سفارش در وضعیت درست نیست."
            };
        }

        // this should be done in one commit by implementing unitOfWork pattern
        order.Status = OrderStatus.ReceivedByCourier;
        _orderRepository.Update(order);
        AddOrderLog(order);

        return new GenericResponse
        {
            IsSuccessful = true,
            Message = "انجام شد"
        };
    }

    public GenericResponse DeliverOrder(int orderId)
    {
        var order = _orderRepository.GetById(orderId);
        var generalCheck = CheckOrderStatusChange(order);
        if (!generalCheck.IsSuccessful)
        {
            return generalCheck;
        }

        if (order.Status != OrderStatus.ReceivedByCourier)
        {
            return new GenericResponse
            {
                IsSuccessful = false,
                Message = "این سفارش در وضعیت درست نیست."
            };
        }

        // this should be done in one commit by implementing unitOfWork pattern
        order.Status = OrderStatus.Delivered;
        _orderRepository.Update(order);
        AddOrderLog(order);

        return new GenericResponse
        {
            IsSuccessful = true,
            Message = "انجام شد"
        };
    }

    public GenericResponse CancelOrder(int orderId)
    {
        var order = _orderRepository.GetById(orderId);
        if (order == null)
        {
            return new GenericResponse
            {
                IsSuccessful = false,
                Message = "سفارش یافت نشد."
            };
        }

        if (!CanOrderBeCancelled(order))
        {
            return new GenericResponse
            {
                IsSuccessful = false,
                Message = "امکان کنسل کردن این سفارش وجود ندارد."
            };
        }

        // this should be done in one commit by implementing unitOfWork pattern
        order.Status = OrderStatus.Cancelled;
        _orderRepository.Update(order);
        AddOrderLog(order);

        return new GenericResponse
        {
            IsSuccessful = true,
            Message = "سفارش کنسل شد"
        };
    }

    private GenericResponse CheckOrderStatusChange(Order order)
    {
        if (order == null)
        {
            return new GenericResponse
            {
                IsSuccessful = false,
                Message = "سفارش یافت نشد."
            };
        }

        if (order.Status == OrderStatus.Cancelled)
        {
            return new GenericResponse
            {
                IsSuccessful = false,
                Message = "این سفارش کنسل شده است."
            };
        }

        if (order.Status == OrderStatus.Delivered)
        {
            return new GenericResponse
            {
                IsSuccessful = false,
                Message = "این سفارش دریافت شده است."
            };
        }

        return new GenericResponse
        {
            IsSuccessful = true
        };
    }
    private void AddOrderLog(Order order)
    {
        var orderLog = new OrderLog
        {
            CreationDate = DateTime.Now,
            Status = order.Status,
            CourierId = order.CourierId,
            OrderId = order.Id
        };
        _orderLogRepository.Add(orderLog);
    }
    private bool CanOrderBeCancelled(Order order)
    {
        return order.Status == OrderStatus.New ||
            order.Status == OrderStatus.Accepted;
    }
}
