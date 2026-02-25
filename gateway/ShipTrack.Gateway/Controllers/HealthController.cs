using Microsoft.AspNetCore.Mvc;

namespace ShipTrack.Gateway.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HealthController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public IActionResult Get() => Ok(new
    {
        Service = "ShipTrack Gateway",
        Status = "Healthy",
        Timestamp = DateTime.UtcNow
    });

    [HttpGet("services")]
    public async Task<IActionResult> GetServicesHealth()
    {
        var services = new[]
        {
            new { Name = "OrderService", Url = "http://localhost:5001/api/health" },
            new { Name = "TrackingService", Url = "http://localhost:5002/api/health" },
            new { Name = "NotificationService", Url = "http://localhost:5003/api/health" }
        };

        var client = _httpClientFactory.CreateClient();
        var results = new List<object>();

        foreach (var service in services)
        {
            try
            {
                var response = await client.GetAsync(service.Url);
                results.Add(new
                {
                    service.Name,
                    Status = response.IsSuccessStatusCode ? "Healthy" : "Unhealthy"
                });
            }
            catch
            {
                results.Add(new { service.Name, Status = "Unreachable" });
            }
        }

        return Ok(new { Gateway = "Healthy", Services = results, Timestamp = DateTime.UtcNow });
    }
}