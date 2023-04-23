using System;
using Microsoft.AspNetCore.Mvc;
using MartinDelivery.Api.Models;
using MartinDelivery.Application.Interfaces;
using MartinDelivery.Api.Utilities;
using MartinDelivery.Api.Auth;

namespace MartinDelivery.Api.Controllers;

[Route("v1/orders")]
[ApiController]
public class OrderController : ControllerBase, IServiceWithOrganization
{
    private IOrderService _orderService;

    public int OrganizationId { get; set; }

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [Route("")]
    [InjectOrganization]
    public IActionResult CreateOrder(CreateOrderModel order)
    {
        if (OrganizationId != order.OrganizationId)
        {
            return Unauthorized();
        }

        var orderDto = order.ToOrderDto();
        var orderId = _orderService.Add(orderDto);
        return Ok(orderId);
    }

    [HttpPost]
    [Route("{orderId}/cancel")]
    [InjectOrganization]
    public IActionResult CancelOrder(int orderId)
    {
        var order = _orderService.GetOrderById(orderId);
        if (OrganizationId != order.OrganizationId)
        {
            return Unauthorized();
        }

        var result = _orderService.CancelOrder(orderId);
        if (result.IsSuccessful)
        {
            // we can inform courier here
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
}
