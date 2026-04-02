using AzShipping.Application.DTOs;
using AzShipping.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzShipping.Presentation.Controllers;

[ApiController]
[Route("api/salesfunnelstatuses")]
[Produces("application/json")]
public class SalesFunnelStatusController : ControllerBase
{
    private readonly ISalesFunnelStatusService _service;

    public SalesFunnelStatusController(ISalesFunnelStatusService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all sales funnel statuses
    /// </summary>
    /// <param name="language">Language code (e.g., EN, LT, RU). Optional query parameter or Accept-Language header.</param>
    /// <returns>List of sales funnel statuses</returns>
    [HttpGet]
    [HttpGet("get/all")]
    [ProducesResponseType(typeof(IEnumerable<SalesFunnelStatusDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SalesFunnelStatusDto>>> GetAll([FromQuery] string? language = null)
    {
        var languageCode = language ?? Request.Headers["Accept-Language"].FirstOrDefault();
        var statuses = await _service.GetAllAsync(languageCode);
        return Ok(statuses);
    }

    /// <summary>
    /// Get sales funnel status by ID
    /// </summary>
    /// <param name="id">Sales funnel status ID</param>
    /// <param name="language">Language code (e.g., EN, LT, RU). Optional query parameter or Accept-Language header.</param>
    /// <returns>Sales funnel status</returns>
    [HttpGet("{id}")]
    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(SalesFunnelStatusDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SalesFunnelStatusDto>> GetById(Guid id, [FromQuery] string? language = null)
    {
        var languageCode = language ?? Request.Headers["Accept-Language"].FirstOrDefault();
        var status = await _service.GetByIdAsync(id, languageCode);
        if (status == null)
            return NotFound(new { message = $"Sales funnel status with ID {id} not found" });

        return Ok(status);
    }

    /// <summary>
    /// Create a new sales funnel status
    /// </summary>
    /// <param name="createDto">Sales funnel status data</param>
    /// <returns>Created sales funnel status</returns>
    [HttpPost]
    [HttpPost("create")]
    [ProducesResponseType(typeof(SalesFunnelStatusDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SalesFunnelStatusDto>> Create([FromBody] CreateSalesFunnelStatusDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Update an existing sales funnel status
    /// </summary>
    /// <param name="id">Sales funnel status ID</param>
    /// <param name="updateDto">Updated sales funnel status data</param>
    /// <returns>Updated sales funnel status</returns>
    [HttpPut("{id}")]
    [HttpPut("update/{id}")]
    [ProducesResponseType(typeof(SalesFunnelStatusDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SalesFunnelStatusDto>> Update(Guid id, [FromBody] UpdateSalesFunnelStatusDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, updateDto);
        if (updated == null)
            return NotFound(new { message = $"Sales funnel status with ID {id} not found" });

        return Ok(updated);
    }

    /// <summary>
    /// Delete a sales funnel status
    /// </summary>
    /// <param name="id">Sales funnel status ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Sales funnel status with ID {id} not found" });

        return NoContent();
    }
}

