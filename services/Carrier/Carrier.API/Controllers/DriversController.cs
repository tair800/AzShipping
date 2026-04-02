using Carrier.Application.DTOs.Driver;
using Carrier.Application.Features.Drivers.Commands.Create;
using Carrier.Application.Features.Drivers.Commands.Delete;
using Carrier.Application.Features.Drivers.Commands.Update;
using Carrier.Application.Features.Drivers.Commands.UpdateDocumentPath;
using Carrier.Application.Features.Drivers.Queries.GetAll;
using Carrier.Application.Features.Drivers.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carrier.API.Controllers;

[ApiController]
[Route("api/drivers")]
public class DriversController(IMediator mediator, IWebHostEnvironment env) : ControllerBase
{
    private static readonly string UploadSubdir = Path.Combine("uploads", "drivers");

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DriverDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllDriversQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DriverDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetDriverByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<DriverDto>> Create([FromBody] CreateDriverDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateDriverCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<DriverDto>> Update(Guid id, [FromBody] UpdateDriverDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateDriverCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteDriverCommand(id), ct);
        return found ? NoContent() : NotFound();
    }

    [HttpPost("{id:guid}/passport")]
    public async Task<IActionResult> UploadPassport(Guid id, IFormFile file, CancellationToken ct)
    {
        if (file == null || file.Length == 0) return BadRequest("No file");
        var relativePath = await SaveDriverFile(id, file, "passport", ct);
        if (relativePath == null) return BadRequest("Save failed");
        var ok = await mediator.Send(new UpdateDriverDocumentPathCommand(id, "passport", relativePath), ct);
        return ok ? Ok(new { path = relativePath }) : NotFound();
    }

    [HttpPost("{id:guid}/drivinglicence")]
    public async Task<IActionResult> UploadDrivingLicence(Guid id, IFormFile file, CancellationToken ct)
    {
        if (file == null || file.Length == 0) return BadRequest("No file");
        var relativePath = await SaveDriverFile(id, file, "drivinglicence", ct);
        if (relativePath == null) return BadRequest("Save failed");
        var ok = await mediator.Send(new UpdateDriverDocumentPathCommand(id, "drivinglicence", relativePath), ct);
        return ok ? Ok(new { path = relativePath }) : NotFound();
    }

    private async Task<string?> SaveDriverFile(Guid driverId, IFormFile file, string prefix, CancellationToken ct)
    {
        var ext = Path.GetExtension(file.FileName)?.TrimStart('.');
        if (string.IsNullOrWhiteSpace(ext)) ext = "bin";
        var safeExt = string.Join("", (ext ?? "bin").Take(10).Where(char.IsLetterOrDigit));
        if (string.IsNullOrEmpty(safeExt)) safeExt = "bin";
        var fileName = $"{prefix}_{DateTime.UtcNow:yyyyMMddHHmmss}.{safeExt}";
        var dir = Path.Combine(env.ContentRootPath, UploadSubdir, driverId.ToString());
        try
        {
            Directory.CreateDirectory(dir);
            var fullPath = Path.Combine(dir, fileName);
            await using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
                await file.CopyToAsync(stream, ct);
            return Path.Combine(UploadSubdir, driverId.ToString(), fileName).Replace('\\', '/');
        }
        catch
        {
            return null;
        }
    }
}

