using Carrier.Application.DTOs.Project;
using Carrier.Application.Features.Projects.Commands.Create;
using Carrier.Application.Features.Projects.Queries.GetByCarrierId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carrier.API.Controllers;

[ApiController]
[Route("api/carriers/{carrierId:guid}/projects")]
public class ProjectsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProjectDto>>> GetByCarrierId(Guid carrierId, CancellationToken ct)
        => Ok(await mediator.Send(new GetProjectsByCarrierIdQuery(carrierId), ct));

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> Create(Guid carrierId, [FromBody] CreateProjectDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateProjectCommand(carrierId, dto), ct);
        return Ok(result);
    }
}

