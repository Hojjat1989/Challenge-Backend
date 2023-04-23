using System;
using Microsoft.AspNetCore.Mvc;
using MartinDelivery.Application.Interfaces;
using MartinDelivery.Api.Models;
using MartinDelivery.Api.Auth;

namespace MartinDelivery.Api.Controllers;

[Route("v1/couriers")]
[ApiController]
public class CourierController : ControllerBase, IServiceWithCourier
{
    private IOrderService _orderService;

    public int CourierId { get; set; }

    public CourierController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [Route("accept-order")]
    [InjectCourier]
    public IActionResult Accept(OrderCourierRequestModel model)
    {
        if (CourierId != model.CourierId)
        {
            return Unauthorized();
        }

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
    [InjectCourier]
    public IActionResult Receive(OrderCourierRequestModel model)
    {
        if (CourierId != model.CourierId)
        {
            return Unauthorized();
        }

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
    [InjectCourier]
    public IActionResult Deliver(OrderCourierRequestModel model)
    {
        if (CourierId != model.CourierId)
        {
            return Unauthorized();
        }

        var result = _orderService.DeliverOrder(model.OrderId);
        if (result.IsSuccessful)
        {
            // todo: inform organization here
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }
}
