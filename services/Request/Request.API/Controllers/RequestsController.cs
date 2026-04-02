using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Request.API.Models;
using Request.Application.DTOs.Request;
using Request.Application.Services;
using Request.Application.Features.Requests.Commands.Create;
using Request.Application.Features.Requests.Commands.Delete;
using Request.Application.Features.Requests.Commands.Update;
using Request.Application.Features.Requests.Queries.GetAll;
using Request.Application.Features.Requests.Queries.GetById;
using Request.Application.Features.Requests.Queries.GetRequestTypes;
using Request.Application.Features.Requests.Queries.GetRequestTypeById;
using Request.Application.Features.Requests.Commands.CreateRequestType;
using Request.Application.Features.Requests.Commands.UpdateRequestType;
using Request.Application.Features.Requests.Commands.DeleteRequestType;

namespace Request.API.Controllers;

[ApiController]
[Route("api/requests")]
public class RequestsController(IMediator mediator) : ControllerBase
{
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<RequestTypeDto>>> GetTypes([FromQuery] bool includeInactive, CancellationToken ct)
        => Ok(await mediator.Send(new GetRequestTypesQuery(includeInactive), ct));

    [HttpGet("types/{id:guid}")]
    public async Task<ActionResult<RequestTypeDto>> GetTypeById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetRequestTypeByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost("types")]
    public async Task<ActionResult<RequestTypeDto>> CreateType([FromBody] CreateRequestTypeDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateRequestTypeCommand(dto), ct);
        return CreatedAtAction(nameof(GetTypeById), new { id = result.Id }, result);
    }

    [HttpPut("types/{id:guid}")]
    public async Task<ActionResult<RequestTypeDto>> UpdateType(Guid id, [FromBody] UpdateRequestTypeDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateRequestTypeCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("types/{id:guid}")]
    public async Task<IActionResult> DeleteType(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteRequestTypeCommand(id), ct);
        return found ? NoContent() : NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RequestDto>>> GetAll([FromQuery] string? type, [FromQuery] string? mode, [FromQuery] string? direction, [FromQuery] string? subType, CancellationToken ct)
        => Ok(await mediator.Send(new GetAllRequestsQuery(type, mode, direction, subType), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetRequestByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost("calculate-dimensions")]
    public ActionResult<CalculateDimensionsResponse> CalculateDimensions([FromBody] CalculateDimensionsRequest request)
    {
        var isSea = string.Equals(request.Mode, "Sea", StringComparison.OrdinalIgnoreCase);
        var isRoad = string.Equals(request.Mode, "Road", StringComparison.OrdinalIgnoreCase);
        var isRail = string.Equals(request.Mode, "Rail", StringComparison.OrdinalIgnoreCase);
        var useSeaFormula = isSea || isRoad || isRail; // Sea, Road, Rail: 1 CBM = 1 MT

        if (request.Dimensions is { Count: > 0 })
        {
            var inputs = request.Dimensions.Select(x => new ExportAirRequestCalculationService.DimensionInput(x.Length, x.Width, x.Height, x.Quantity, x.WeightKg)).ToList();

            if (useSeaFormula)
            {
                var totals = SeaFreightCalculationService.CalculateTotals(inputs);
                var rows = totals.Rows.Select(r => new DimensionRowResult(r.VolumeCbm, r.VolumetricWeightMt)).ToList();
                return Ok(new CalculateDimensionsResponse(
                    totals.TotalGrossWeightKg,
                    totals.TotalVolumeCbm,
                    totals.TotalVolumetricWeightMt,
                    totals.ChargeableWeightMt,
                    totals.NumberOfPackages,
                    rows));
            }
            else
            {
                var totals = ExportAirRequestCalculationService.CalculateTotals(inputs);
                var rows = totals.Rows.Select(r => new DimensionRowResult(r.VolumeCbm, r.VolumetricWeightKg)).ToList();
                return Ok(new CalculateDimensionsResponse(
                    totals.TotalGrossWeightKg,
                    totals.TotalVolumeCbm,
                    totals.TotalVolumetricWeightKg,
                    totals.ChargeableWeightKg,
                    totals.NumberOfPackages,
                    rows));
            }
        }

        if (request.VolumeCbm.HasValue && request.VolumeCbm > 0)
        {
            decimal volWeight, chargeable;
            if (useSeaFormula)
            {
                volWeight = SeaFreightCalculationService.CalculateVolumetricWeightMt(request.VolumeCbm.Value);
                chargeable = SeaFreightCalculationService.RoundChargeableWeight(volWeight);
            }
            else
            {
                volWeight = (decimal)request.VolumeCbm * ExportAirRequestCalculationService.VolumetricFactorKgPerCbm;
                chargeable = ExportAirRequestCalculationService.CalculateChargeableFromManualVolume(request.VolumeCbm) ?? 0;
            }
            return Ok(new CalculateDimensionsResponse(
                request.GrossWeightKg ?? 0,
                request.VolumeCbm.Value,
                volWeight,
                chargeable,
                0,
                null));
        }

        return Ok(new CalculateDimensionsResponse(0, 0, 0, 0, 0, null));
    }

    [HttpPost]
    public async Task<ActionResult<RequestDto>> Create([FromBody] CreateRequestDto dto, CancellationToken ct)
    {
        try
        {
            var result = await mediator.Send(new CreateRequestCommand(dto), ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RequestDto>> Update(Guid id, [FromBody] UpdateRequestDto dto, CancellationToken ct)
    {
        try
        {
            var result = await mediator.Send(new UpdateRequestCommand(id, dto), ct);
            return result == null ? NotFound() : Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteRequestCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

