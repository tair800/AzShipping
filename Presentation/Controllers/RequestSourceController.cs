using AzShipping.Application.DTOs;
using AzShipping.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzShipping.Presentation.Controllers;

[ApiController]
[Route("api/requestsources")]
[Produces("application/json")]
public class RequestSourceController : ControllerBase
{
    private readonly IRequestSourceService _service;

    public RequestSourceController(IRequestSourceService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all request sources
    /// </summary>
    /// <returns>List of request sources</returns>
    [HttpGet]
    [HttpGet("get/all")]
    [ProducesResponseType(typeof(IEnumerable<RequestSourceDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RequestSourceDto>>> GetAll()
    {
        var sources = await _service.GetAllAsync();
        return Ok(sources);
    }

    /// <summary>
    /// Get request source by ID
    /// </summary>
    /// <param name="id">Request source ID</param>
    /// <returns>Request source</returns>
    [HttpGet("{id}")]
    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(RequestSourceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RequestSourceDto>> GetById(Guid id)
    {
        var source = await _service.GetByIdAsync(id);
        if (source == null)
            return NotFound(new { message = $"Request source with ID {id} not found" });

        return Ok(source);
    }

    /// <summary>
    /// Create a new request source
    /// </summary>
    /// <param name="createDto">Request source data</param>
    /// <returns>Created request source</returns>
    [HttpPost]
    [HttpPost("create")]
    [ProducesResponseType(typeof(RequestSourceDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RequestSourceDto>> Create([FromBody] CreateRequestSourceDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Update an existing request source
    /// </summary>
    /// <param name="id">Request source ID</param>
    /// <param name="updateDto">Updated request source data</param>
    /// <returns>Updated request source</returns>
    [HttpPut("{id}")]
    [HttpPut("update/{id}")]
    [ProducesResponseType(typeof(RequestSourceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RequestSourceDto>> Update(Guid id, [FromBody] UpdateRequestSourceDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, updateDto);
        if (updated == null)
            return NotFound(new { message = $"Request source with ID {id} not found" });

        return Ok(updated);
    }

    /// <summary>
    /// Delete a request source
    /// </summary>
    /// <param name="id">Request source ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Request source with ID {id} not found" });

        return NoContent();
    }
}

