using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Settings.API.Options;
using Settings.API.Security;
using Settings.Application.DTOs.EmailSetting;
using Settings.Application.Features.EmailSettings.Commands.Create;
using Settings.Application.Features.EmailSettings.Commands.Delete;
using Settings.Application.Features.EmailSettings.Commands.SendSystem;
using Settings.Application.Features.EmailSettings.Commands.TestMailbox;
using Settings.Application.Features.EmailSettings.Commands.LinkIdentityUser;
using Settings.Application.Features.EmailSettings.Commands.Update;
using Settings.Application.Features.EmailSettings.Queries.GetAll;
using Settings.Application.Features.EmailSettings.Queries.GetById;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/email-settings")]
public sealed class EmailSettingsController(IMediator mediator, IOptions<EmailSystemSendOptions> systemEmailSendOptions) : ControllerBase
{
    private readonly EmailSystemSendOptions _systemEmailSend = systemEmailSendOptions.Value;
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllEmailSettingsQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetEmailSettingByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmailSettingDto dto, CancellationToken ct)
    {
        try
        {
            var result = await mediator.Send(new CreateEmailSettingCommand(dto), ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>Bind or clear <see cref="EmailSettingDetailDto.IdentityUserId"/> without resubmitting SMTP fields (Identity user create / admin UI).</summary>
    [HttpPatch("{id:guid}/link-identity-user")]
    public async Task<IActionResult> LinkIdentityUser(Guid id, [FromBody] LinkIdentityUserToEmailSettingDto dto, CancellationToken ct)
    {
        try
        {
            var result = await mediator.Send(new LinkIdentityUserToEmailSettingCommand(id, dto), ct);
            return result == null ? NotFound() : Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmailSettingDto dto, CancellationToken ct)
    {
        try
        {
            var result = await mediator.Send(new UpdateEmailSettingCommand(id, dto), ct);
            return result == null ? NotFound() : Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var ok = await mediator.Send(new DeleteEmailSettingCommand(id), ct);
        return ok ? NoContent() : NotFound();
    }

    [HttpPost("{id:guid}/test")]
    public async Task<IActionResult> TestMailbox(Guid id, [FromBody] TestEmailMailboxDto? dto, CancellationToken ct)
    {
        try
        {
            await mediator.Send(new TestEmailMailboxCommand(id, dto ?? new TestEmailMailboxDto(null)), ct);
            return Ok(new { ok = true });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>Identity (and other internal services) send transactional mail via the first <c>IsSystemEmail</c> mailbox. Secured by <see cref="SystemEmailSendAuth.HeaderName"/>.</summary>
    [HttpPost("system/send")]
    [AllowAnonymous]
    public async Task<IActionResult> SendSystemEmail([FromBody] SendSystemEmailDto dto, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(_systemEmailSend.ApiKey))
            return StatusCode(503, "EmailSystemSend:ApiKey is not configured.");

        if (!Request.Headers.TryGetValue(SystemEmailSendAuth.HeaderName, out var keyHeader) ||
            !SystemEmailSendAuth.IsAuthorized(_systemEmailSend.ApiKey, keyHeader.ToString()))
            return Unauthorized();

        try
        {
            await mediator.Send(new SendSystemEmailCommand(dto), ct);
            return Ok(new { ok = true });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
