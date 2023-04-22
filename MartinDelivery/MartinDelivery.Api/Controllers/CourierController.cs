using System;
using Microsoft.AspNetCore.Mvc;
using MartinDelivery.Application.Interfaces;

namespace MartinDelivery.Api.Controllers;

[Route("v1/couriers")]
[ApiController]
public class CourierController : ControllerBase
{
    private IOrderService _orderService;

    public CourierController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [Route("{courierId}/orders/{orderId}")]
    public IActionResult Accept(int courierId, int orderId)
    {
        var result = _orderService.AcceptOrder(orderId, courierId);
        if (result.IsSuccessful)
        {
            // todo: inform organization here
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
}
