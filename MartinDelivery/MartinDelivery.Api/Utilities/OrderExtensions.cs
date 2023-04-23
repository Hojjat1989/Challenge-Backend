using System;
using MartinDelivery.Api.Models;
using MartinDelivery.Application.DTOs;

namespace MartinDelivery.Api.Utilities;

public static class OrderExtensions
{
    public static OrderListModel ToOrderListModel(this OrderListDto orderList)
    {
        return new OrderListModel
        {
            HasMore = orderList.HasMore,
            TotalCount = orderList.TotalCount,
            Orders = orderList.Orders.Select(x => x.ToOrderModel()).ToArray()
        };
    }

    public static OrderModel ToOrderModel(this OrderDto order)
    {
        return new OrderModel
        {
            Id = order.Id,
            CreationDate = order.CreationDate,
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
            SenderPhone = order.SenderPhone
        };
    }

    public static OrderDto ToOrderDto(this CreateOrderModel model)
    {
        return new OrderDto
        {
            CreationDate = DateTime.Now,
            OrganizationId = model.OrganizationId,
            SenderName = model.SenderName,
            SenderPhone = model.SenderPhone,
            SenderAddress = model.SenderAddress,
            SenderLatitude = model.SenderLatitude,
            SenderLongitude = model.SenderLongitude,
            ReceiverName = model.ReceiverName,
            ReceiverPhone = model.ReceiverPhone,
            ReceiverAddress = model.ReceiverAddress,
            ReceiverLatitude = model.ReceiverLatitude,
            ReceiverLongitude = model.ReceiverLongitude,
        };
    }
}
