using System;
using Microsoft.AspNetCore.Mvc;
using MartinDelivery.Api.Models;
using MartinDelivery.Api.Auth;
using MartinDelivery.Api.Utilities;
using MartinDelivery.Api.Webhooks;
using MartinDelivery.Application;

namespace MartinDelivery.Api.Controllers;

[Route("v1/couriers")]
[ApiController]
public class CourierController : ControllerBase, IServiceWithCourier
{
    private IOrderService _orderService;
    private IWebhookPublisher _webhookPublisher;

    public int CourierId { get; set; }

    public CourierController(IOrderService orderService,
        IWebhookPublisher webhookPublisher)
    {
        _orderService = orderService;
        _webhookPublisher = webhookPublisher;
    }

    [HttpGet]
    [Route("available-orders")]
    [InjectCourier]
    public IActionResult GetAvailableOrders(int offset, int size)
    {
        var orderList = _orderService.GetAvailableOrders(offset, size);
        return Ok(orderList.ToOrderListModel());
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
            _webhookPublisher.OrderStatusChanged(model.OrderId, model.CourierLocation);
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
            _webhookPublisher.OrderStatusChanged(model.OrderId, model.CourierLocation);
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
            _webhookPublisher.OrderStatusChanged(model.OrderId, model.CourierLocation);
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }

    [HttpPost]
    [Route("set-location")]
    [InjectCourier]
    public IActionResult SetLocation(Location location)
    {
        var courierOrders = _orderService.GetCourierOrders(CourierId);
        foreach (var order in courierOrders)
        {
            _webhookPublisher.OrderStatusChanged(order, location);
        }

        return Ok();
    }
}
