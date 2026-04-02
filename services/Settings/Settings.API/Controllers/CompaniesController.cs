using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.Company;
using Settings.Application.Features.Companies;
using Settings.Domain.AggregatesModel.CompanyAggregate;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController(IMediator mediator, ICompanyRepository repository, IWebHostEnvironment env) : ControllerBase
{
    private static readonly string UploadSubdir = Path.Combine("uploads", "companies");
    private static readonly string[] AllowedTypes = ["Seal", "Logo", "Signature"];

    private string GetUploadDir(Guid companyId) => Path.Combine(env.ContentRootPath, UploadSubdir, companyId.ToString());

    private async Task<string?> SaveSignatureFile(Guid companyId, string type, IFormFile file, CancellationToken ct)
    {
        var ext = Path.GetExtension(file.FileName)?.TrimStart('.') ?? "bin";
        var safeExt = string.Join("", ext.Take(10).Where(char.IsLetterOrDigit));
        if (string.IsNullOrEmpty(safeExt)) safeExt = "bin";
        var fileName = $"{type.ToLowerInvariant()}_{DateTime.UtcNow:yyyyMMddHHmmss}.{safeExt}";
        var dir = GetUploadDir(companyId);
        try
        {
            Directory.CreateDirectory(dir);
            var fullPath = Path.Combine(dir, fileName);
            await using var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None);
            await file.CopyToAsync(stream, ct);
            return Path.Combine(UploadSubdir, companyId.ToString(), fileName).Replace('\\', '/');
        }
        catch
        {
            return null;
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllCompaniesQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetCompanyByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompanyDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateCompanyCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCompanyDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateCompanyCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var success = await mediator.Send(new DeleteCompanyCommand(id), ct);
        return success ? NoContent() : NotFound();
    }

    [HttpPost("{id:guid}/signatures/upload")]
    public async Task<IActionResult> UploadSignature(Guid id, [FromQuery] string type, IFormFile file, CancellationToken ct)
    {
        if (file == null || file.Length == 0) return BadRequest("No file");
        if (!AllowedTypes.Contains(type, StringComparer.OrdinalIgnoreCase)) return BadRequest("Invalid type. Use Seal, Logo, or Signature");
        var company = await repository.GetByIdAsync(id, ct);
        if (company == null) return NotFound();
        var relativePath = await SaveSignatureFile(id, type, file, ct);
        if (relativePath == null) return BadRequest("Save failed");
        var signature = await repository.UpsertSignatureAsync(id, type, file.FileName, relativePath, ct);
        return Ok(new { path = relativePath, fileName = file.FileName, id = signature?.Id });
    }

    [HttpGet("{id:guid}/signatures/{type}/file")]
    public async Task<IActionResult> GetSignatureFile(Guid id, string type, CancellationToken ct)
    {
        var company = await repository.GetByIdAsync(id, ct);
        if (company == null) return NotFound();
        var sig = company.Signatures.FirstOrDefault(s => string.Equals(s.Type, type, StringComparison.OrdinalIgnoreCase));
        if (sig?.FilePath == null) return NotFound();
        var fullPath = Path.Combine(env.ContentRootPath, sig.FilePath.Replace('/', Path.DirectorySeparatorChar));
        if (!System.IO.File.Exists(fullPath)) return NotFound();
        var contentType = type switch
        {
            _ when fullPath.EndsWith(".png", StringComparison.OrdinalIgnoreCase) => "image/png",
            _ when fullPath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || fullPath.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) => "image/jpeg",
            _ when fullPath.EndsWith(".gif", StringComparison.OrdinalIgnoreCase) => "image/gif",
            _ when fullPath.EndsWith(".webp", StringComparison.OrdinalIgnoreCase) => "image/webp",
            _ when fullPath.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) => "application/pdf",
            _ => "application/octet-stream"
        };
        return PhysicalFile(fullPath, contentType, sig.FileName ?? "file");
    }

    [HttpDelete("{id:guid}/signatures/{type}")]
    public async Task<IActionResult> DeleteSignature(Guid id, string type, CancellationToken ct)
    {
        var company = await repository.GetByIdAsync(id, ct);
        if (company == null) return NotFound();
        var sig = company.Signatures.FirstOrDefault(s => string.Equals(s.Type, type, StringComparison.OrdinalIgnoreCase));
        if (sig?.FilePath != null)
        {
            var fullPath = Path.Combine(env.ContentRootPath, sig.FilePath.Replace('/', Path.DirectorySeparatorChar));
            if (System.IO.File.Exists(fullPath))
                try { System.IO.File.Delete(fullPath); } catch { /* ignore */ }
        }
        await repository.DeleteSignatureAsync(id, type, ct);
        return NoContent();
    }
}

