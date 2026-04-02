using Clients.Application.DTOs.Document;
using Clients.Application.Features.Documents.Commands.Create;
using Clients.Application.Features.Documents.Commands.Delete;
using Clients.Application.Features.Documents.Commands.Update;
using Clients.Application.Features.Documents.Queries.GetByClientId;
using Clients.Application.Features.Documents.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clients.API.Controllers;

[ApiController]
[Route("api/documents")]
public class DocumentsController(IMediator mediator, IWebHostEnvironment env) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DocumentDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetDocumentByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet("by-client/{clientId:guid}")]
    public async Task<ActionResult<IReadOnlyList<DocumentDto>>> GetByClientId(Guid clientId, CancellationToken ct)
        => Ok(await mediator.Send(new GetDocumentsByClientIdQuery(clientId), ct));

    [HttpPost]
    public async Task<ActionResult<DocumentDto>> Create([FromBody] CreateDocumentDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateDocumentCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPost("upload")]
    public async Task<ActionResult<DocumentDto>> Upload([FromForm] Guid clientId, [FromForm] string? companyId, [FromForm] string? documentNumber,
        [FromForm] string? documentDate, [FromForm] string? documentName, [FromForm] string? validFrom, [FromForm] string? validUntil,
        [FromForm] bool prohibitOnExpiry, [FromForm] bool isDefault, [FromForm] string? comments, [FromForm] bool availableForClient, [FromForm] bool isSent,
        IFormFile? file, CancellationToken ct)
    {
        Guid? companyIdParsed = Guid.TryParse(companyId, out var cid) ? cid : null;
        DateTime? validFromParsed = DateTime.TryParse(validFrom, null, System.Globalization.DateTimeStyles.RoundtripKind, out var vf) ? vf : null;
        DateTime? validUntilParsed = DateTime.TryParse(validUntil, null, System.Globalization.DateTimeStyles.RoundtripKind, out var vu) ? vu : null;
        var dto = new CreateDocumentDto
        {
            ClientId = clientId,
            CompanyId = companyIdParsed,
            DocumentType = "upload",
            DocumentNumber = documentNumber ?? "",
            DocumentDate = documentDate,
            DocumentName = documentName ?? (file?.FileName ?? "Uploaded file"),
            ValidFrom = validFromParsed,
            ValidUntil = validUntilParsed,
            ProhibitOnExpiry = prohibitOnExpiry,
            IsDefault = isDefault,
            Comments = comments,
            AvailableForClient = availableForClient,
            IsSent = isSent
        };
        var result = await mediator.Send(new CreateDocumentCommand(dto), ct);
        if (file != null && file.Length > 0)
        {
            var uploadsDir = Path.Combine(env.ContentRootPath, "uploads", "documents");
            Directory.CreateDirectory(uploadsDir);
            var ext = string.IsNullOrEmpty(Path.GetExtension(file.FileName)) ? ".bin" : Path.GetExtension(file.FileName);
            var fileName = $"{result.Id}{ext}";
            var fullPath = Path.Combine(uploadsDir, fileName);
            await using (var fs = System.IO.File.Create(fullPath))
                await file.CopyToAsync(fs, ct);
            var storedPath = $"documents/{fileName}";
            var d = await mediator.Send(new GetDocumentByIdQuery(result.Id), ct);
            if (d != null)
            {
                await mediator.Send(new UpdateDocumentCommand(result.Id, new UpdateDocumentDto
                {
                    CompanyId = d.CompanyId,
                    DocumentType = "upload",
                    DocumentNumber = d.DocumentNumber,
                    DocumentDate = d.DocumentDate.ToString("yyyy-MM-dd"),
                    DocumentName = d.DocumentName,
                    ValidFrom = d.ValidFrom,
                    ValidUntil = d.ValidUntil,
                    ProhibitOnExpiry = d.ProhibitOnExpiry,
                    IsDefault = d.IsDefault,
                    Comments = d.Comments,
                    AvailableForClient = d.AvailableForClient,
                    IsSent = d.IsSent,
                    FilePath = storedPath
                }), ct);
            }
            var doc = await mediator.Send(new GetDocumentByIdQuery(result.Id), ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, doc ?? result);
        }
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPost("{id:guid}/file")]
    public async Task<ActionResult<DocumentDto>> UploadFile(Guid id, IFormFile file, CancellationToken ct)
    {
        var existing = await mediator.Send(new GetDocumentByIdQuery(id), ct);
        if (existing == null) return NotFound();
        if (file == null || file.Length == 0) return BadRequest("No file provided");
        var uploadsDir = Path.Combine(env.ContentRootPath, "uploads", "documents");
        Directory.CreateDirectory(uploadsDir);
        var ext = string.IsNullOrEmpty(Path.GetExtension(file.FileName)) ? ".bin" : Path.GetExtension(file.FileName);
        var fileName = $"{id}{ext}";
        var fullPath = Path.Combine(uploadsDir, fileName);
        await using (var fs = System.IO.File.Create(fullPath))
            await file.CopyToAsync(fs, ct);
        var storedPath = $"documents/{fileName}";
        var result = await mediator.Send(new UpdateDocumentCommand(id, new UpdateDocumentDto
        {
            CompanyId = existing.CompanyId,
            DocumentType = existing.DocumentType,
            DocumentNumber = existing.DocumentNumber,
            DocumentDate = existing.DocumentDate.ToString("yyyy-MM-dd"),
            DocumentName = existing.DocumentName,
            ValidFrom = existing.ValidFrom,
            ValidUntil = existing.ValidUntil,
            ProhibitOnExpiry = existing.ProhibitOnExpiry,
            IsDefault = existing.IsDefault,
            Comments = existing.Comments,
            AvailableForClient = existing.AvailableForClient,
            IsSent = existing.IsSent,
            FilePath = storedPath
        }), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<DocumentDto>> Update(Guid id, [FromBody] UpdateDocumentDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateDocumentCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteDocumentCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

