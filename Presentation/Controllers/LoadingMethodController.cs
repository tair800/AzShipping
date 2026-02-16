using AzShipping.Application.DTOs;
using AzShipping.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzShipping.Presentation.Controllers;

[ApiController]
[Route("api/loadingmethods")]
[Produces("application/json")]
public class LoadingMethodController : ControllerBase
{
    private readonly ILoadingMethodService _service;

    public LoadingMethodController(ILoadingMethodService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all loading methods
    /// </summary>
    /// <param name="language">Language code (e.g., EN, LT, RU). Optional query parameter or Accept-Language header.</param>
    /// <returns>List of loading methods</returns>
    [HttpGet]
    [HttpGet("get/all")]
    [ProducesResponseType(typeof(IEnumerable<LoadingMethodDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LoadingMethodDto>>> GetAll([FromQuery] string? language = null)
    {
        // Get language from query parameter, header, or default to null
        var languageCode = language ?? Request.Headers["Accept-Language"].FirstOrDefault();
        var loadingMethods = await _service.GetAllAsync(languageCode);
        return Ok(loadingMethods);
    }

    /// <summary>
    /// Get loading method by ID
    /// </summary>
    /// <param name="id">Loading method ID</param>
    /// <param name="language">Language code (e.g., EN, LT, RU). Optional query parameter or Accept-Language header.</param>
    /// <returns>Loading method</returns>
    [HttpGet("{id}")]
    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(LoadingMethodDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LoadingMethodDto>> GetById(Guid id, [FromQuery] string? language = null)
    {
        // Get language from query parameter, header, or default to null
        var languageCode = language ?? Request.Headers["Accept-Language"].FirstOrDefault();
        var loadingMethod = await _service.GetByIdAsync(id, languageCode);
        if (loadingMethod == null)
            return NotFound(new { message = $"Loading method with ID {id} not found" });

        return Ok(loadingMethod);
    }

    /// <summary>
    /// Create a new loading method
    /// </summary>
    /// <param name="createDto">Loading method data</param>
    /// <returns>Created loading method</returns>
    [HttpPost]
    [HttpPost("create")]
    [ProducesResponseType(typeof(LoadingMethodDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LoadingMethodDto>> Create([FromBody] CreateLoadingMethodDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Update an existing loading method
    /// </summary>
    /// <param name="id">Loading method ID</param>
    /// <param name="updateDto">Updated loading method data</param>
    /// <returns>Updated loading method</returns>
    [HttpPut("{id}")]
    [HttpPut("update/{id}")]
    [ProducesResponseType(typeof(LoadingMethodDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LoadingMethodDto>> Update(Guid id, [FromBody] UpdateLoadingMethodDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, updateDto);
        if (updated == null)
            return NotFound(new { message = $"Loading method with ID {id} not found" });

        return Ok(updated);
    }

    /// <summary>
    /// Delete a loading method
    /// </summary>
    /// <param name="id">Loading method ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Loading method with ID {id} not found" });

        return NoContent();
    }
}

