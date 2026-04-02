using Accounting.Application.DTOs.OperationAct;
using Accounting.Application.Features.OperationActs.Commands.Create;
using Accounting.Application.Features.OperationActs.Commands.Delete;
using Accounting.Application.Features.OperationActs.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers;

[ApiController]
[Route("api/operation-acts")]
[Route("api/operation-act")] // singular alias (tools / typos)
public class OperationActsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyList<OperationActListItemDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllOperationActsQuery(), ct));

    [HttpPost]
    public async Task<ActionResult<OperationActListItemDto>> Create([FromBody] CreateOperationActDto dto, CancellationToken ct)
    {
        var created = await mediator.Send(new CreateOperationActCommand(dto), ct);
        return StatusCode(StatusCodes.Status201Created, created);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken ct)
    {
        var ok = await mediator.Send(new DeleteOperationActCommand(id), ct);
        return ok ? NoContent() : NotFound();
    }
}
