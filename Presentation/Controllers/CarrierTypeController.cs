using AzShipping.Application.DTOs;
using AzShipping.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzShipping.Presentation.Controllers;

[ApiController]
[Route("api/carriertypes")]
[Produces("application/json")]
public class CarrierTypeController : ControllerBase
{
    private readonly ICarrierTypeService _service;

    public CarrierTypeController(ICarrierTypeService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all carrier types
    /// </summary>
    /// <returns>List of carrier types</returns>
    [HttpGet]
    [HttpGet("get/all")]
    [ProducesResponseType(typeof(IEnumerable<CarrierTypeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CarrierTypeDto>>> GetAll()
    {
        var carrierTypes = await _service.GetAllAsync();
        return Ok(carrierTypes);
    }

    /// <summary>
    /// Get carrier type by ID
    /// </summary>
    /// <param name="id">Carrier type ID</param>
    /// <returns>Carrier type</returns>
    [HttpGet("{id}")]
    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(CarrierTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarrierTypeDto>> GetById(Guid id)
    {
        var carrierType = await _service.GetByIdAsync(id);
        if (carrierType == null)
            return NotFound(new { message = $"Carrier type with ID {id} not found" });

        return Ok(carrierType);
    }

    /// <summary>
    /// Create a new carrier type
    /// </summary>
    /// <param name="createDto">Carrier type data</param>
    /// <returns>Created carrier type</returns>
    [HttpPost]
    [HttpPost("create")]
    [ProducesResponseType(typeof(CarrierTypeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CarrierTypeDto>> Create([FromBody] CreateCarrierTypeDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Update an existing carrier type
    /// </summary>
    /// <param name="id">Carrier type ID</param>
    /// <param name="updateDto">Updated carrier type data</param>
    /// <returns>Updated carrier type</returns>
    [HttpPut("{id}")]
    [HttpPut("update/{id}")]
    [ProducesResponseType(typeof(CarrierTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CarrierTypeDto>> Update(Guid id, [FromBody] UpdateCarrierTypeDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, updateDto);
        if (updated == null)
            return NotFound(new { message = $"Carrier type with ID {id} not found" });

        return Ok(updated);
    }

    /// <summary>
    /// Delete a carrier type
    /// </summary>
    /// <param name="id">Carrier type ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Carrier type with ID {id} not found" });

        return NoContent();
    }
}

