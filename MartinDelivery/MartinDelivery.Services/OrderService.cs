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

        var orderLog = new OrderLog
        {
            CreationDate = DateTime.Now,
            Status = OrderStatus.New,
            OrderId = order.Id
        };
        _orderLogRepository.Add(orderLog);

        return orderEntity.Id;
    }
    public GenericResponse AcceptOrder(int orderId, int courierId)
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

        if (order.CourierId.HasValue)
        {
            return new GenericResponse
            {
                IsSuccessful = false,
                Message = "این سفارش در دسترس نیست."
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

        order.Status = OrderStatus.Accepted;
        order.CourierId = courierId;
        _orderRepository.Update(order);

        var orderLog = new OrderLog
        {
            CreationDate = DateTime.Now,
            Status = OrderStatus.Accepted,
            OrderId = order.Id,
            CourierId = courierId
        };
        _orderLogRepository.Add(orderLog);

        return new GenericResponse
        {
            IsSuccessful = true,
            Message = "انجام شد"
        };
    }
    public GenericResponse ReceiveOrder(int orderId)
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

        if (order.Status == OrderStatus.Cancelled)
        {
            return new GenericResponse
            {
                IsSuccessful = false,
                Message = "این سفارش کنسل شده است."
            };
        }

        if (order.Status != OrderStatus.Accepted)
        {
            return new GenericResponse
            {
                IsSuccessful = false,
                Message = "این سفارش در وضعیت درست نیست."
            };
        }

        order.Status = OrderStatus.ReceivedByCourier;
        _orderRepository.Update(order);
        var orderLog = new OrderLog
        {
            CreationDate = DateTime.Now,
            Status = OrderStatus.ReceivedByCourier,
            OrderId = order.Id
        };
        _orderLogRepository.Add(orderLog);

        return new GenericResponse
        {
            IsSuccessful = true,
            Message = "انجام شد"
        };
    }
    public GenericResponse DeliverOrder(int orderId)
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

        if (order.Status == OrderStatus.Cancelled)
        {
            return new GenericResponse
            {
                IsSuccessful = false,
                Message = "این سفارش کنسل شده است."
            };
        }

        if (order.Status != OrderStatus.ReceivedByCourier)
        {
            return new GenericResponse
            {
                IsSuccessful = false,
                Message = "این سفارش در وضعیت درست نیست."
            };
        }

        order.Status = OrderStatus.Delivered;
        _orderRepository.Update(order);
        var orderLog = new OrderLog
        {
            CreationDate = DateTime.Now,
            Status = OrderStatus.Delivered,
            OrderId = order.Id
        };
        _orderLogRepository.Add(orderLog);

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

        order.Status = OrderStatus.Cancelled;
        var orderLog = new OrderLog
        {
            CreationDate = DateTime.Now,
            Status = OrderStatus.Cancelled,
            OrderId = order.Id
        };

        // this should be done in one commit by implementing unitOfWork pattern
        _orderRepository.Update(order);
        _orderLogRepository.Add(orderLog);

        return new GenericResponse
        {
            IsSuccessful = true,
            Message = "سفارش کنسل شد"
        };
    }

    private bool CanOrderBeCancelled(Order order)
    {
        return order.Status == OrderStatus.New ||
            order.Status == OrderStatus.Accepted;
    }
}
