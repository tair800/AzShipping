using Microsoft.AspNetCore.Mvc;

namespace AzShipping.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    /// <summary>
    /// Test endpoint to verify the API is working
    /// </summary>
    /// <returns>A simple test message</returns>
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { message = "AzShipping API is working!", timestamp = DateTime.UtcNow });
    }
}

