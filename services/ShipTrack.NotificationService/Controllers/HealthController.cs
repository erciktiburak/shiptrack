using Microsoft.AspNetCore.Mvc;

namespace ShipTrack.NotificationService.Controllers; 

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new
    {
        Service = "NotificationService", 
        Status = "Healthy",
        Timestamp = DateTime.UtcNow
    });
}