using Microsoft.AspNetCore.Mvc;

namespace ShipTrack.OrderService.Controllers; 

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new
    {
        Service = "OrderService", 
        Status = "Healthy",
        Timestamp = DateTime.UtcNow
    });
}