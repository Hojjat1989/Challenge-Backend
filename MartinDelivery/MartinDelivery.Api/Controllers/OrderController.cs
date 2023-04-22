using System;
using Microsoft.AspNetCore.Mvc;
using MartinDelivery.Api.Models;
using MartinDelivery.Application.Interfaces;
using MartinDelivery.Api.Utilities;

namespace MartinDelivery.Api.Controllers;

[Route("v1/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [Route("")]
    public IActionResult CreateOrder(CreateOrderModel order)
    {
        var orderDto = order.ToOrderDto();
        var orderId = _orderService.Add(orderDto);
        return Ok(orderId);
    }

    [HttpPost]
    [Route("{orderId}/cancel")]
    public IActionResult CancelOrder(int orderId)
    {
        var result = _orderService.CancelOrder(orderId);
        if (result.IsSuccessful)
        {
            // we can inform courier here
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
}
