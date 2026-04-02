using AzShipping.Application.DTOs;
using AzShipping.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzShipping.Presentation.Controllers;

[ApiController]
[Route("api/banks")]
public class BankController : ControllerBase
{
    private readonly BankService _bankService;

    public BankController(BankService bankService)
    {
        _bankService = bankService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BankDto>>> GetAll()
    {
        var banks = await _bankService.GetAllAsync();
        return Ok(banks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BankDto>> GetById(Guid id)
    {
        var bank = await _bankService.GetByIdAsync(id);
        if (bank == null)
            return NotFound();

        return Ok(bank);
    }

    [HttpPost]
    public async Task<ActionResult<BankDto>> Create([FromBody] CreateBankDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var bank = await _bankService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = bank.Id }, bank);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BankDto>> Update(Guid id, [FromBody] UpdateBankDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var bank = await _bankService.UpdateAsync(id, updateDto);
        if (bank == null)
            return NotFound();

        return Ok(bank);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _bankService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}

