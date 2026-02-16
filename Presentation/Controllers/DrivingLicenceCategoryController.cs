using AzShipping.Application.DTOs;
using AzShipping.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzShipping.Presentation.Controllers;

[ApiController]
[Route("api/drivinglicencecategories")]
[Produces("application/json")]
public class DrivingLicenceCategoryController : ControllerBase
{
    private readonly IDrivingLicenceCategoryService _service;

    public DrivingLicenceCategoryController(IDrivingLicenceCategoryService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all driving licence categories
    /// </summary>
    /// <returns>List of driving licence categories</returns>
    [HttpGet]
    [HttpGet("get/all")]
    [ProducesResponseType(typeof(IEnumerable<DrivingLicenceCategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DrivingLicenceCategoryDto>>> GetAll()
    {
        var categories = await _service.GetAllAsync();
        return Ok(categories);
    }

    /// <summary>
    /// Get driving licence category by ID
    /// </summary>
    /// <param name="id">Driving licence category ID</param>
    /// <returns>Driving licence category</returns>
    [HttpGet("{id}")]
    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(DrivingLicenceCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DrivingLicenceCategoryDto>> GetById(Guid id)
    {
        var category = await _service.GetByIdAsync(id);
        if (category == null)
            return NotFound(new { message = $"Driving licence category with ID {id} not found" });

        return Ok(category);
    }

    /// <summary>
    /// Create a new driving licence category
    /// </summary>
    /// <param name="createDto">Driving licence category data</param>
    /// <returns>Created driving licence category</returns>
    [HttpPost]
    [HttpPost("create")]
    [ProducesResponseType(typeof(DrivingLicenceCategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DrivingLicenceCategoryDto>> Create([FromBody] CreateDrivingLicenceCategoryDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Update an existing driving licence category
    /// </summary>
    /// <param name="id">Driving licence category ID</param>
    /// <param name="updateDto">Updated driving licence category data</param>
    /// <returns>Updated driving licence category</returns>
    [HttpPut("{id}")]
    [HttpPut("update/{id}")]
    [ProducesResponseType(typeof(DrivingLicenceCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DrivingLicenceCategoryDto>> Update(Guid id, [FromBody] UpdateDrivingLicenceCategoryDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, updateDto);
        if (updated == null)
            return NotFound(new { message = $"Driving licence category with ID {id} not found" });

        return Ok(updated);
    }

    /// <summary>
    /// Delete a driving licence category
    /// </summary>
    /// <param name="id">Driving licence category ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Driving licence category with ID {id} not found" });

        return NoContent();
    }
}

