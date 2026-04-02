using Carrier.Application.DTOs.CarrierDocument;
using Carrier.Application.Features.CarrierDocuments.Commands.Create;
using Carrier.Application.Features.CarrierDocuments.Commands.Delete;
using Carrier.Application.Features.CarrierDocuments.Commands.Update;
using Carrier.Application.Features.CarrierDocuments.Queries.GetById;
using Carrier.Application.Features.CarrierDocuments.Queries.GetByCarrierId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carrier.API.Controllers;

[ApiController]
[Route("api/carriers/{carrierId:guid}/documents")]
public class CarrierDocumentsController(IMediator mediator, IWebHostEnvironment env) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CarrierDocumentDto>>> GetByCarrierId(Guid carrierId, CancellationToken ct)
        => Ok(await mediator.Send(new GetCarrierDocumentsQuery(carrierId), ct));

    [HttpGet("{id:guid}", Name = nameof(GetCarrierDocumentById))]
    public async Task<ActionResult<CarrierDocumentDto>> GetCarrierDocumentById(Guid carrierId, Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetCarrierDocumentByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CarrierDocumentDto>> Create(Guid carrierId, [FromBody] CreateCarrierDocumentDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateCarrierDocumentCommand(carrierId, dto), ct);
        return CreatedAtRoute(nameof(GetCarrierDocumentById), new { carrierId, id = result.Id }, result);
    }

    [HttpPost("{id:guid}/file")]
    public async Task<ActionResult<CarrierDocumentDto>> UploadFile(Guid carrierId, Guid id, IFormFile file, CancellationToken ct)
    {
        var existing = await mediator.Send(new GetCarrierDocumentByIdQuery(id), ct);
        if (existing == null) return NotFound();
        if (file == null || file.Length == 0) return BadRequest("No file provided");

        var uploadsDir = Path.Combine(env.ContentRootPath, "uploads", "carrier-documents");
        Directory.CreateDirectory(uploadsDir);
        var ext = string.IsNullOrEmpty(Path.GetExtension(file.FileName)) ? ".bin" : Path.GetExtension(file.FileName);
        var fileName = $"{id}{ext}";
        var fullPath = Path.Combine(uploadsDir, fileName);
        await using (var fs = System.IO.File.Create(fullPath))
            await file.CopyToAsync(fs, ct);
        var storedPath = $"carrier-documents/{fileName}";

        var result = await mediator.Send(new UpdateCarrierDocumentCommand(id, new UpdateCarrierDocumentDto
        {
            DocumentNumber = existing.DocumentNumber,
            DocumentDate = existing.DocumentDate,
            DocumentName = existing.DocumentName,
            ExpirationDate = existing.ExpirationDate,
            Comments = existing.Comments,
            AvailableForClient = existing.AvailableForClient,
            IsSent = existing.IsSent,
            FilePath = storedPath
        }), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CarrierDocumentDto>> Update(Guid carrierId, Guid id, [FromBody] UpdateCarrierDocumentDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateCarrierDocumentCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid carrierId, Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteCarrierDocumentCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

