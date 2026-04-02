using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.ResultType;
using Settings.Application.Features.ResultTypes;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/resulttypes")]
public class ResultTypesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ResultTypeDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllResultTypesQuery(), ct));
}

