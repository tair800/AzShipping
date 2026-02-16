using AzShipping.Application.DTOs;
using AzShipping.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzShipping.Presentation.Controllers;

[ApiController]
[Route("api/requestpurposes")]
[Produces("application/json")]
public class RequestPurposeController : ControllerBase
{
    private readonly IRequestPurposeService _service;

    public RequestPurposeController(IRequestPurposeService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all request purposes
    /// </summary>
    /// <returns>List of request purposes</returns>
    [HttpGet]
    [HttpGet("get/all")]
    [ProducesResponseType(typeof(IEnumerable<RequestPurposeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RequestPurposeDto>>> GetAll()
    {
        var purposes = await _service.GetAllAsync();
        return Ok(purposes);
    }

    /// <summary>
    /// Get request purpose by ID
    /// </summary>
    /// <param name="id">Request purpose ID</param>
    /// <returns>Request purpose</returns>
    [HttpGet("{id}")]
    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(RequestPurposeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RequestPurposeDto>> GetById(Guid id)
    {
        var purpose = await _service.GetByIdAsync(id);
        if (purpose == null)
            return NotFound(new { message = $"Request purpose with ID {id} not found" });

        return Ok(purpose);
    }

    /// <summary>
    /// Create a new request purpose
    /// </summary>
    /// <param name="createDto">Request purpose data</param>
    /// <returns>Created request purpose</returns>
    [HttpPost]
    [HttpPost("create")]
    [ProducesResponseType(typeof(RequestPurposeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RequestPurposeDto>> Create([FromBody] CreateRequestPurposeDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Update an existing request purpose
    /// </summary>
    /// <param name="id">Request purpose ID</param>
    /// <param name="updateDto">Updated request purpose data</param>
    /// <returns>Updated request purpose</returns>
    [HttpPut("{id}")]
    [HttpPut("update/{id}")]
    [ProducesResponseType(typeof(RequestPurposeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RequestPurposeDto>> Update(Guid id, [FromBody] UpdateRequestPurposeDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, updateDto);
        if (updated == null)
            return NotFound(new { message = $"Request purpose with ID {id} not found" });

        return Ok(updated);
    }

    /// <summary>
    /// Delete a request purpose
    /// </summary>
    /// <param name="id">Request purpose ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Request purpose with ID {id} not found" });

        return NoContent();
    }
}

