using MartinDelivery.Api.Models;
using MartinDelivery.Application.DTOs;

namespace MartinDelivery.Api.Utilities;

public static class OrderExtensions
{
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
