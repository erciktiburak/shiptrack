using Microsoft.AspNetCore.Mvc;
using ShipTrack.NotificationService.Services;

namespace ShipTrack.NotificationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet("{trackingNumber}/logs")]
    public async Task<IActionResult> GetLogs(string trackingNumber)
    {
        var logs = await _notificationService.GetLogsAsync(trackingNumber);
        return Ok(logs);
    }
}