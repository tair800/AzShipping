using Microsoft.AspNetCore.Mvc;

namespace General.API.Controllers;

/// <summary>
/// Placeholder for payroll / serial (s/n) calculation APIs. Enforces <c>Calculation.*</c> ERP claims until the aggregate exists.
/// </summary>
[ApiController]
[Route("api/salary-calculations")]
public sealed class SalaryCalculationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll() => Ok(Array.Empty<object>());
}
