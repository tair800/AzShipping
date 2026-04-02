using General.Application.DTOs.Project;
using General.Application.Features.Projects.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace General.API.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProjectDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllProjectsQuery(), ct));
}

