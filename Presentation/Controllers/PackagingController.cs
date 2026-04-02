using AzShipping.Application.DTOs;
using AzShipping.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzShipping.Presentation.Controllers;

[ApiController]
[Route("api/packagings")]
[Produces("application/json")]
public class PackagingController : ControllerBase
{
    private readonly IPackagingService _service;

    public PackagingController(IPackagingService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all packagings
    /// </summary>
    /// <param name="language">Language code (e.g., EN, LT, RU). Optional query parameter or Accept-Language header.</param>
    /// <returns>List of packagings</returns>
    [HttpGet]
    [HttpGet("get/all")]
    [ProducesResponseType(typeof(IEnumerable<PackagingDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PackagingDto>>> GetAll([FromQuery] string? language = null)
    {
        // Get language from query parameter, header, or default to null
        var languageCode = language ?? Request.Headers["Accept-Language"].FirstOrDefault();
        var packagings = await _service.GetAllAsync(languageCode);
        return Ok(packagings);
    }

    /// <summary>
    /// Get packaging by ID
    /// </summary>
    /// <param name="id">Packaging ID</param>
    /// <param name="language">Language code (e.g., EN, LT, RU). Optional query parameter or Accept-Language header.</param>
    /// <returns>Packaging</returns>
    [HttpGet("{id}")]
    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(PackagingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PackagingDto>> GetById(Guid id, [FromQuery] string? language = null)
    {
        // Get language from query parameter, header, or default to null
        var languageCode = language ?? Request.Headers["Accept-Language"].FirstOrDefault();
        var packaging = await _service.GetByIdAsync(id, languageCode);
        if (packaging == null)
            return NotFound(new { message = $"Packaging with ID {id} not found" });

        return Ok(packaging);
    }

    /// <summary>
    /// Create a new packaging
    /// </summary>
    /// <param name="createDto">Packaging data</param>
    /// <returns>Created packaging</returns>
    [HttpPost]
    [HttpPost("create")]
    [ProducesResponseType(typeof(PackagingDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PackagingDto>> Create([FromBody] CreatePackagingDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Update an existing packaging
    /// </summary>
    /// <param name="id">Packaging ID</param>
    /// <param name="updateDto">Updated packaging data</param>
    /// <returns>Updated packaging</returns>
    [HttpPut("{id}")]
    [HttpPut("update/{id}")]
    [ProducesResponseType(typeof(PackagingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PackagingDto>> Update(Guid id, [FromBody] UpdatePackagingDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, updateDto);
        if (updated == null)
            return NotFound(new { message = $"Packaging with ID {id} not found" });

        return Ok(updated);
    }

    /// <summary>
    /// Delete a packaging
    /// </summary>
    /// <param name="id">Packaging ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Packaging with ID {id} not found" });

        return NoContent();
    }
}

