using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class HealthCheckController : ControllerBase
{
    [HttpPost]
    [Produces("application/json")]
    public ScoreResponse Post([FromBody] HealthCheck healthCheck)
    {
        return Service.createScoreResponse(healthCheck);
    }
}