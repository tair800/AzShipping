using AzShipping.Application.DTOs;
using AzShipping.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzShipping.Presentation.Controllers;

[ApiController]
[Route("api/workerposts")]
[Produces("application/json")]
public class WorkerPostController : ControllerBase
{
    private readonly IWorkerPostService _service;

    public WorkerPostController(IWorkerPostService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all worker posts
    /// </summary>
    /// <param name="language">Language code (e.g., EN, LT, RU). Optional query parameter or Accept-Language header.</param>
    /// <returns>List of worker posts</returns>
    [HttpGet]
    [HttpGet("get/all")]
    [ProducesResponseType(typeof(IEnumerable<WorkerPostDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<WorkerPostDto>>> GetAll([FromQuery] string? language = null)
    {
        // Get language from query parameter, header, or default to null
        var languageCode = language ?? Request.Headers["Accept-Language"].FirstOrDefault();
        var posts = await _service.GetAllAsync(languageCode);
        return Ok(posts);
    }

    /// <summary>
    /// Get worker post by ID
    /// </summary>
    /// <param name="id">Worker post ID</param>
    /// <param name="language">Language code (e.g., EN, LT, RU). Optional query parameter or Accept-Language header.</param>
    /// <returns>Worker post</returns>
    [HttpGet("{id}")]
    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(WorkerPostDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WorkerPostDto>> GetById(Guid id, [FromQuery] string? language = null)
    {
        // Get language from query parameter, header, or default to null
        var languageCode = language ?? Request.Headers["Accept-Language"].FirstOrDefault();
        var post = await _service.GetByIdAsync(id, languageCode);
        if (post == null)
            return NotFound(new { message = $"Worker post with ID {id} not found" });

        return Ok(post);
    }

    /// <summary>
    /// Create a new worker post
    /// </summary>
    /// <param name="createDto">Worker post data</param>
    /// <returns>Created worker post</returns>
    [HttpPost]
    [HttpPost("create")]
    [ProducesResponseType(typeof(WorkerPostDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WorkerPostDto>> Create([FromBody] CreateWorkerPostDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Update an existing worker post
    /// </summary>
    /// <param name="id">Worker post ID</param>
    /// <param name="updateDto">Updated worker post data</param>
    /// <returns>Updated worker post</returns>
    [HttpPut("{id}")]
    [HttpPut("update/{id}")]
    [ProducesResponseType(typeof(WorkerPostDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WorkerPostDto>> Update(Guid id, [FromBody] UpdateWorkerPostDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, updateDto);
        if (updated == null)
            return NotFound(new { message = $"Worker post with ID {id} not found" });

        return Ok(updated);
    }

    /// <summary>
    /// Delete a worker post
    /// </summary>
    /// <param name="id">Worker post ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Worker post with ID {id} not found" });

        return NoContent();
    }
}

