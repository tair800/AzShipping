using MediatR;
using Microsoft.AspNetCore.Mvc;
using Operation.Application.DTOs.Operation;
using Operation.Application.Features.Operations;
using Operation.Application.Services;

namespace Operation.API.Controllers;

[ApiController]
[Route("api/operations")]
public class OperationsController(IMediator mediator) : ControllerBase
{
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<OperationTypeDto>>> GetTypes([FromQuery] bool includeInactive = false, CancellationToken ct = default)
        => Ok(await mediator.Send(new GetOperationTypesQuery(includeInactive), ct));

    [HttpGet("types/{id:guid}")]
    public async Task<ActionResult<OperationTypeDto>> GetTypeById(Guid id, CancellationToken ct)
    {
        var r = await mediator.Send(new GetOperationTypeByIdQuery(id), ct);
        return r == null ? NotFound() : Ok(r);
    }

    /// <summary>Preview gross/volume/chargeable from dimension rows or manual CBM (air: 166.67 kg/CBM; sea LCL, rail LCL &amp; road LTL/OOG: 1000 kg/CBM — same as save).</summary>
    [HttpPost("calculate-air-dimensions")]
    public ActionResult<CalculateAirDimensionsResponse> CalculateAirDimensions([FromBody] CalculateAirDimensionsRequest request)
    {
        var factor = request.UseSeaLclVolumetricFactor
            ? AirFreightCalculationService.SeaLclVolumetricFactorKgPerCbm
            : AirFreightCalculationService.VolumetricFactorKgPerCbm;

        if (request.Dimensions is { Count: > 0 })
        {
            var inputs = request.Dimensions
                .Select(x => new AirFreightCalculationService.DimensionInput(x.Length, x.Width, x.Height, x.Quantity, x.WeightKg, x.VolumeCbm))
                .ToList();
            var t = AirFreightCalculationService.CalculateTotals(inputs, factor);
            return Ok(new CalculateAirDimensionsResponse(
                t.TotalGrossWeightKg > 0 ? t.TotalGrossWeightKg : null,
                t.TotalVolumeCbm > 0 ? t.TotalVolumeCbm : null,
                t.ChargeableWeightKg > 0 ? t.ChargeableWeightKg : null,
                t.NumberOfPackages > 0 ? t.NumberOfPackages : null,
                t.TotalVolumetricWeightKg > 0 ? t.TotalVolumetricWeightKg : null));
        }

        if (request.VolumeCbm is > 0)
        {
            var c = AirFreightCalculationService.CalculateChargeableFromManualVolume(request.VolumeCbm, factor);
            var vw = request.VolumeCbm.Value * factor;
            return Ok(new CalculateAirDimensionsResponse(null, request.VolumeCbm, c, null,
                vw > 0 ? Math.Round(vw, 3, MidpointRounding.AwayFromZero) : null));
        }

        return Ok(new CalculateAirDimensionsResponse(null, null, null, null, null));
    }

    /// <summary>Compute line subtotals, VAT, VAT-inclusive totals, and profit — single source of truth for finance UI.</summary>
    [HttpPost("calculate-finance")]
    public ActionResult<CalculateFinanceAmountsResponse> CalculateFinanceAmounts([FromBody] CalculateFinanceAmountsRequest? request)
    {
        request ??= new CalculateFinanceAmountsRequest(null, null);
        FinanceLineCalculationDto? incomeDto = null;
        FinanceLineCalculationDto? expenseDto = null;

        if (request.Income != null)
        {
            var r = FinanceCalculationService.CalculateLine(
                request.Income.Amount,
                request.Income.UnitPrice,
                request.Income.VatRatePercent);
            incomeDto = new FinanceLineCalculationDto(r.LineSubtotal, r.VatAmount, r.TotalWithVat);
        }

        if (request.Expense != null)
        {
            var r = FinanceCalculationService.CalculateLine(
                request.Expense.Amount,
                request.Expense.UnitPrice,
                request.Expense.VatRatePercent);
            expenseDto = new FinanceLineCalculationDto(r.LineSubtotal, r.VatAmount, r.TotalWithVat);
        }

        decimal? profitEx = null;
        decimal? profitIncl = null;
        if (incomeDto != null && expenseDto != null)
        {
            profitEx = Math.Round(incomeDto.LineSubtotal - expenseDto.LineSubtotal, 2, MidpointRounding.AwayFromZero);
            profitIncl = Math.Round(incomeDto.TotalWithVat - expenseDto.TotalWithVat, 2, MidpointRounding.AwayFromZero);
        }

        return Ok(new CalculateFinanceAmountsResponse(incomeDto, expenseDto, profitEx, profitIncl));
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OperationDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllOperationsQuery(), ct));

    /// <summary>Slim rows for the operations list grid (Figma): status, route, cargo summary, freight/VAS/profit hints.</summary>
    [HttpGet("list")]
    public async Task<ActionResult<IReadOnlyList<OperationListItemDto>>> GetList(CancellationToken ct)
        => Ok(await mediator.Send(new GetOperationsListQuery(), ct));

    /// <summary>Trips grid (Figma): one row per operation until trip legs are a separate aggregate.</summary>
    [HttpGet("trips-list")]
    public async Task<ActionResult<IReadOnlyList<TripListItemDto>>> GetTripsList(CancellationToken ct)
        => Ok(await mediator.Send(new GetTripsListQuery(), ct));

    /// <summary>Cargos grid (Figma): same projection as trips-list — cargo is modeled on the operation until a separate cargo leg entity exists.</summary>
    [HttpGet("cargos-list")]
    public async Task<ActionResult<IReadOnlyList<TripListItemDto>>> GetCargosList(CancellationToken ct)
        => Ok(await mediator.Send(new GetTripsListQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OperationDto>> GetById(Guid id, CancellationToken ct)
    {
        var r = await mediator.Send(new GetOperationByIdQuery(id), ct);
        return r == null ? NotFound() : Ok(r);
    }

    [HttpPost]
    public async Task<ActionResult<OperationDto>> Create([FromBody] SaveOperationDto dto, CancellationToken ct)
    {
        var r = await mediator.Send(new CreateOperationCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<OperationDto>> Update(Guid id, [FromBody] SaveOperationDto dto, CancellationToken ct)
    {
        var r = await mediator.Send(new UpdateOperationCommand(id, dto), ct);
        return r == null ? NotFound() : Ok(r);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var ok = await mediator.Send(new DeleteOperationCommand(id), ct);
        return ok ? NoContent() : NotFound();
    }
}
