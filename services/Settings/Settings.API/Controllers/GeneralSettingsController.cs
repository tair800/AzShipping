using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.GeneralSetting;
using Settings.Application.Features.GeneralSettings.Commands.Update;
using Settings.Application.Features.GeneralSettings.Queries.Get;
using Settings.Domain.AggregatesModel.GeneralSettingAggregate;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/general-settings")]
public class GeneralSettingsController(IMediator mediator, IGeneralSettingRepository repository, IWebHostEnvironment env) : ControllerBase
{
    private static readonly string UploadSubdir = Path.Combine("uploads", "general-settings");

    [HttpGet]
    public async Task<ActionResult<GeneralSettingDto>> Get(CancellationToken ct)
    {
        var result = await mediator.Send(new GetGeneralSettingQuery(), ct);
        if (result == null)
        {
            var entity = await repository.GetOrCreateAsync(ct);
            result = new GeneralSettingDto(
                entity.Id, entity.LogoPath, entity.CurrencyCode, entity.DateFormat, entity.PriceDisplayType,
                entity.DefaultLanguageCode, entity.NotificationLanguageCode, entity.BankCode, entity.Timezone,
                entity.UseCreditLimit, entity.CreatedAt, entity.UpdatedAt);
        }
        return Ok(result);
    }

    [HttpGet("price-display-types")]
    public ActionResult<string[]> GetPriceDisplayTypes() => Ok(PriceDisplayTypeOptions.All);

    [HttpPut]
    public async Task<ActionResult<GeneralSettingDto>> Update([FromBody] UpdateGeneralSettingDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateGeneralSettingCommand(dto), ct);
        return Ok(result);
    }

    [HttpPost("logo/upload")]
    public async Task<IActionResult> UploadLogo(IFormFile file, CancellationToken ct)
    {
        if (file == null || file.Length == 0) return BadRequest("No file");
        var entity = await repository.GetOrCreateAsync(ct);
        var ext = Path.GetExtension(file.FileName)?.TrimStart('.') ?? "png";
        var safeExt = string.Join("", ext.Take(10).Where(char.IsLetterOrDigit));
        if (string.IsNullOrEmpty(safeExt)) safeExt = "png";
        var fileName = $"logo_{DateTime.UtcNow:yyyyMMddHHmmss}.{safeExt}";
        var dir = Path.Combine(env.ContentRootPath, UploadSubdir);
        try
        {
            Directory.CreateDirectory(dir);
            var fullPath = Path.Combine(dir, fileName);
            await using var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None);
            await file.CopyToAsync(stream, ct);
            var relativePath = Path.Combine(UploadSubdir, fileName).Replace('\\', '/');
            entity.LogoPath = relativePath;
            await repository.SaveAsync(entity, ct);
            return Ok(new { path = relativePath, fileName });
        }
        catch
        {
            return BadRequest("Save failed");
        }
    }

    [HttpDelete("logo")]
    public async Task<IActionResult> DeleteLogo(CancellationToken ct)
    {
        var entity = await repository.GetOrCreateAsync(ct);
        if (!string.IsNullOrEmpty(entity.LogoPath))
        {
            var fullPath = Path.Combine(env.ContentRootPath, entity.LogoPath.Replace('/', Path.DirectorySeparatorChar));
            if (System.IO.File.Exists(fullPath))
            {
                try { System.IO.File.Delete(fullPath); } catch { /* ignore */ }
            }
            entity.LogoPath = null;
            await repository.SaveAsync(entity, ct);
        }
        return NoContent();
    }

    [HttpGet("logo")]
    public async Task<IActionResult> GetLogo(CancellationToken ct)
    {
        var entity = await repository.GetAsync(ct);
        if (entity?.LogoPath == null) return NotFound();
        var fullPath = Path.Combine(env.ContentRootPath, entity.LogoPath.Replace('/', Path.DirectorySeparatorChar));
        if (!System.IO.File.Exists(fullPath)) return NotFound();
        var contentType = fullPath.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ? "image/png" :
            fullPath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || fullPath.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ? "image/jpeg" :
            fullPath.EndsWith(".svg", StringComparison.OrdinalIgnoreCase) ? "image/svg+xml" : "application/octet-stream";
        return PhysicalFile(fullPath, contentType);
    }
}
