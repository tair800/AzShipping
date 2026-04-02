using AzShipping.Application.DTOs;
using AzShipping.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzShipping.Presentation.Controllers;

[ApiController]
[Route("api/deferredpaymentconditions")]
[Produces("application/json")]
public class DeferredPaymentConditionController : ControllerBase
{
    private readonly IDeferredPaymentConditionService _service;

    public DeferredPaymentConditionController(IDeferredPaymentConditionService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all deferred payment conditions
    /// </summary>
    /// <returns>List of deferred payment conditions</returns>
    [HttpGet]
    [HttpGet("get/all")]
    [ProducesResponseType(typeof(IEnumerable<DeferredPaymentConditionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DeferredPaymentConditionDto>>> GetAll()
    {
        var conditions = await _service.GetAllAsync();
        return Ok(conditions);
    }

    /// <summary>
    /// Get deferred payment condition by ID
    /// </summary>
    /// <param name="id">Deferred payment condition ID</param>
    /// <returns>Deferred payment condition</returns>
    [HttpGet("{id}")]
    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(DeferredPaymentConditionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeferredPaymentConditionDto>> GetById(Guid id)
    {
        var condition = await _service.GetByIdAsync(id);
        if (condition == null)
            return NotFound(new { message = $"Deferred payment condition with ID {id} not found" });

        return Ok(condition);
    }

    /// <summary>
    /// Create a new deferred payment condition
    /// </summary>
    /// <param name="createDto">Deferred payment condition data</param>
    /// <returns>Created deferred payment condition</returns>
    [HttpPost]
    [HttpPost("create")]
    [ProducesResponseType(typeof(DeferredPaymentConditionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DeferredPaymentConditionDto>> Create([FromBody] CreateDeferredPaymentConditionDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Update an existing deferred payment condition
    /// </summary>
    /// <param name="id">Deferred payment condition ID</param>
    /// <param name="updateDto">Updated deferred payment condition data</param>
    /// <returns>Updated deferred payment condition</returns>
    [HttpPut("{id}")]
    [HttpPut("update/{id}")]
    [ProducesResponseType(typeof(DeferredPaymentConditionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DeferredPaymentConditionDto>> Update(Guid id, [FromBody] UpdateDeferredPaymentConditionDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, updateDto);
        if (updated == null)
            return NotFound(new { message = $"Deferred payment condition with ID {id} not found" });

        return Ok(updated);
    }

    /// <summary>
    /// Delete a deferred payment condition
    /// </summary>
    /// <param name="id">Deferred payment condition ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Deferred payment condition with ID {id} not found" });

        return NoContent();
    }
}

