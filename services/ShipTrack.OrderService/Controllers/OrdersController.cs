using Microsoft.AspNetCore.Mvc;
using ShipTrack.OrderService.DTOs;
using ShipTrack.OrderService.Services;
using Microsoft.AspNetCore.Authorization;


namespace ShipTrack.OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _orderService.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _orderService.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("track/{trackingNumber}")]
    public async Task<IActionResult> GetByTracking(string trackingNumber)
    {
        var result = await _orderService.GetByTrackingNumberAsync(trackingNumber);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateShipmentDto dto)
    {
        var result = await _orderService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpDelete("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var success = await _orderService.CancelAsync(id);
        return success ? NoContent() : NotFound();
    }
}