using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Request.API.Controllers;

[ApiController]
[Route("api")]
public class HealthController : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("health")]
    public IActionResult Health() => Ok(new { service = "Request.API", status = "ok" });
}

