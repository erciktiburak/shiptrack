using Microsoft.AspNetCore.Mvc;
using ShipTrack.TrackingService.DTOs;
using ShipTrack.TrackingService.Services;

namespace ShipTrack.TrackingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrackingController : ControllerBase
{
    private readonly ITrackingService _trackingService;

    public TrackingController(ITrackingService trackingService)
    {
        _trackingService = trackingService;
    }

    [HttpGet("{trackingNumber}/history")]
    public async Task<IActionResult> GetHistory(string trackingNumber)
    {
        var records = await _trackingService.GetHistoryAsync(trackingNumber);
        return Ok(records);
    }

    [HttpGet("{trackingNumber}/latest")]
    public async Task<IActionResult> GetLatest(string trackingNumber)
    {
        var record = await _trackingService.GetLatestAsync(trackingNumber);
        return record is null ? NotFound() : Ok(record);
    }

    [HttpPost("{shipmentId:guid}/{trackingNumber}/update")]
    public async Task<IActionResult> UpdateLocation(
        Guid shipmentId,
        string trackingNumber,
        [FromBody] UpdateLocationDto dto)
    {
        var record = await _trackingService.AddRecordAsync(shipmentId, trackingNumber, dto);
        return Ok(record);
    }
}