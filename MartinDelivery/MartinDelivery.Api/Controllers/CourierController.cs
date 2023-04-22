using System;
using Microsoft.AspNetCore.Mvc;
using MartinDelivery.Application.Interfaces;
using MartinDelivery.Api.Models;

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
    [Route("accept-order")]
    public IActionResult Accept(AcceptOrderModel model)
    {
        var result = _orderService.AcceptOrder(model.OrderId, model.CourierId);
        if (result.IsSuccessful)
        {
            // todo: inform organization here
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }

    [HttpPost]
    [Route("receive-order")]
    public IActionResult Receive(AcceptOrderModel model)
    {
        var result = _orderService.ReceiveOrder(model.OrderId);
        if (result.IsSuccessful)
        {
            // todo: inform organization here
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }

    [HttpPost]
    [Route("deliver-order")]
    public IActionResult Deliver(AcceptOrderModel model)
    {
        var result = _orderService.DeliverOrder(model.OrderId);
        if (result.IsSuccessful)
        {
            // todo: inform organization here
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
}
