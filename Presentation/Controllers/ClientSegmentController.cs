using AzShipping.Application.DTOs;
using AzShipping.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzShipping.Presentation.Controllers;

[ApiController]
[Route("api/clientsegments")]
[Produces("application/json")]
public class ClientSegmentController : ControllerBase
{
    private readonly IClientSegmentService _service;

    public ClientSegmentController(IClientSegmentService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all client segments
    /// </summary>
    /// <returns>List of client segments</returns>
    [HttpGet]
    [HttpGet("get/all")]
    [ProducesResponseType(typeof(IEnumerable<ClientSegmentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ClientSegmentDto>>> GetAll()
    {
        var segments = await _service.GetAllAsync();
        return Ok(segments);
    }

    /// <summary>
    /// Get client segment by ID
    /// </summary>
    /// <param name="id">Client segment ID</param>
    /// <returns>Client segment</returns>
    [HttpGet("{id}")]
    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(ClientSegmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientSegmentDto>> GetById(Guid id)
    {
        var segment = await _service.GetByIdAsync(id);
        if (segment == null)
            return NotFound(new { message = $"Client segment with ID {id} not found" });

        return Ok(segment);
    }

    /// <summary>
    /// Create a new client segment
    /// </summary>
    /// <param name="createDto">Client segment data</param>
    /// <returns>Created client segment</returns>
    [HttpPost]
    [HttpPost("create")]
    [ProducesResponseType(typeof(ClientSegmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClientSegmentDto>> Create([FromBody] CreateClientSegmentDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Update an existing client segment
    /// </summary>
    /// <param name="id">Client segment ID</param>
    /// <param name="updateDto">Updated client segment data</param>
    /// <returns>Updated client segment</returns>
    [HttpPut("{id}")]
    [HttpPut("update/{id}")]
    [ProducesResponseType(typeof(ClientSegmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClientSegmentDto>> Update(Guid id, [FromBody] UpdateClientSegmentDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, updateDto);
        if (updated == null)
            return NotFound(new { message = $"Client segment with ID {id} not found" });

        return Ok(updated);
    }

    /// <summary>
    /// Delete a client segment
    /// </summary>
    /// <param name="id">Client segment ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Client segment with ID {id} not found" });

        return NoContent();
    }
}

