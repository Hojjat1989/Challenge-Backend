using System;
using MartinDelivery.Application.DTOs;
using MartinDelivery.Core.Entities;

namespace MartinDelivery.Services.Mappings;

internal static class OrderExtensions
{
    public static OrderDto ToOrderDto(this Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            CreationDate = order.CreationDate,
            CourierId = order.CourierId,
            OrganizationId = order.OrganizationId,
            ReceiverAddress = order.ReceiverAddress,
            ReceiverLatitude = order.ReceiverLatitude,
            ReceiverLongitude = order.ReceiverLongitude,
            ReceiverName = order.ReceiverName,
            ReceiverPhone = order.ReceiverPhone,
            SenderAddress = order.SenderAddress,
            SenderLatitude = order.SenderLatitude,
            SenderLongitude = order.SenderLongitude,
            SenderName = order.SenderName,
            SenderPhone = order.SenderPhone,
            Status = order.Status.ToOrderDtoStatus()
        };
    }

    public static Order ToOrder(this OrderDto order)
    {
        return new Order
        {
            Id = order.Id,
            CreationDate = order.CreationDate,
            CourierId = order.CourierId,
            OrganizationId = order.OrganizationId,
            ReceiverAddress = order.ReceiverAddress,
            ReceiverLatitude = order.ReceiverLatitude,
            ReceiverLongitude = order.ReceiverLongitude,
            ReceiverName = order.ReceiverName,
            ReceiverPhone = order.ReceiverPhone,
            SenderAddress = order.SenderAddress,
            SenderLatitude = order.SenderLatitude,
            SenderLongitude = order.SenderLongitude,
            SenderName = order.SenderName,
            SenderPhone = order.SenderPhone,
            Status = order.Status.ToOrderStatus()
        };
    }

    public static OrderDtoStatus ToOrderDtoStatus(this OrderStatus status)
    {
        switch (status)
        {
            case OrderStatus.New:
                return OrderDtoStatus.New;
            case OrderStatus.Accepted:
                return OrderDtoStatus.Accepted;
            case OrderStatus.ReceivedByCourier:
                return OrderDtoStatus.ReceivedByCourier;
            case OrderStatus.Delivered:
                return OrderDtoStatus.Delivered;
            case OrderStatus.Cancelled:
                return OrderDtoStatus.Cancelled;
            default:
                throw new ArgumentException();
        }
    }

    public static OrderStatus ToOrderStatus(this OrderDtoStatus status)
    {
        switch (status)
        {
            case OrderDtoStatus.New:
                return OrderStatus.New;
            case OrderDtoStatus.Accepted:
                return OrderStatus.Accepted;
            case OrderDtoStatus.ReceivedByCourier:
                return OrderStatus.ReceivedByCourier;
            case OrderDtoStatus.Delivered:
                return OrderStatus.Delivered;
            case OrderDtoStatus.Cancelled:
                return OrderStatus.Cancelled;
            default:
                throw new ArgumentException();
        }
    }
}
