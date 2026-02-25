using Microsoft.AspNetCore.Mvc;

namespace ShipTrack.TrackingService.Controllers; 

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new
    {
        Service = "TrackingService", 
        Status = "Healthy",
        Timestamp = DateTime.UtcNow
    });
}