using System.Text;
using ClosedXML.Excel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quotes.Application.DTOs.Dimension;
using Quotes.Application.DTOs.Quote;
using Quotes.Application.Features.Quotes.Commands.CreateQuote;
using Quotes.Application.Features.Quotes.Commands.DeleteQuote;
using Quotes.Application.Features.Quotes.Commands.UpdateQuote;
using Quotes.Application.Features.Quotes.Queries.GetAllQuotes;
using Quotes.Application.Features.Quotes.Queries.GetQuoteById;
using Quotes.Application.Features.Quotes.Queries.GetQuoteFunnel;
using Quotes.Application.Features.Quotes.Queries.GetQuoteTypes;
using Quotes.Application.Services;

namespace Quotes.API.Controllers;

[ApiController]
[Route("api/quotes")]
public class QuotesController(IMediator mediator) : ControllerBase
{
    [HttpPost("calculate-dimensions")]
    public ActionResult<CalculateDimensionsResponse> CalculateDimensions([FromBody] CalculateDimensionsRequest request)
    {
        if (request.Dimensions is { Count: > 0 })
        {
            var inputs = request.Dimensions
                .Select(x => new DimensionCalculationService.DimensionInput(x.Length, x.Width, x.Height, x.Quantity, x.WeightKg))
                .ToList();
            var mode = request.Mode ?? "Sea";
            var result = DimensionCalculationService.Calculate(inputs, mode);
            var rows = result.Rows?.Select(r => new DimensionRowResult(r.VolumeCbm, r.VolumetricWeightKg)).ToList();
            return Ok(new CalculateDimensionsResponse(
                result.TotalGrossWeightKg,
                result.TotalVolumeCbm,
                result.TotalVolumetricWeightKg,
                result.ChargeableWeightKg,
                result.NumberOfPackages,
                rows));
        }

        if (request.VolumeCbm.HasValue && request.VolumeCbm > 0)
        {
            var mode = request.Mode ?? "Sea";
            var result = DimensionCalculationService.CalculateFromManualVolume(
                request.VolumeCbm.Value, request.GrossWeightKg, mode);
            return Ok(new CalculateDimensionsResponse(
                result.TotalGrossWeightKg,
                result.TotalVolumeCbm,
                result.TotalVolumetricWeightKg,
                result.ChargeableWeightKg,
                result.NumberOfPackages,
                null));
        }

        return Ok(new CalculateDimensionsResponse(0, 0, 0, 0, 0, null));
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<QuoteTypeDto>>> GetTypes([FromQuery] bool includeInactive, CancellationToken ct)
        => Ok(await mediator.Send(new GetQuoteTypesQuery(includeInactive), ct));

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<QuoteDto>>> GetAll([FromQuery] string? mode, [FromQuery] string? direction, [FromQuery] string? subType, CancellationToken ct)
        => Ok(await mediator.Send(new GetAllQuotesQuery(mode, direction, subType), ct));

    [HttpGet("export")]
    public async Task<IActionResult> ExportToExcel([FromQuery] string? mode, [FromQuery] string? direction, [FromQuery] string? subType, CancellationToken ct)
    {
        var quotes = await mediator.Send(new GetAllQuotesQuery(mode, direction, subType), ct);

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Quotes");

        // Header row
        var headers = new[]
        {
            "ID",
            "Creation date",
            "Quote number",
            "Client",
            "Carrier",
            "Start date",
            "Expiration date",
            "Cargo information",
            "Loading place",
            "Unloading place",
            "Deal value",
            "Manager",
            "Quote number (display)"
        };

        for (var i = 0; i < headers.Length; i++)
        {
            worksheet.Cell(1, i + 1).Value = headers[i];
        }

        // Data rows
        var row = 2;
        foreach (var q in quotes)
        {
            var cargoInfo = q.DescriptionOfGoods;
            if (string.IsNullOrWhiteSpace(cargoInfo))
            {
                if (q.Quantity1.HasValue && !string.IsNullOrWhiteSpace(q.PackageType1))
                {
                    cargoInfo = $"{q.Quantity1} x {q.PackageType1}";
                }
            }

            var loadingPlace = q.PickupCityName
                               ?? q.PickupCountryName
                               ?? q.GatewayName
                               ?? q.MyPortName
                               ?? string.Empty;

            var unloadingPlace = q.DeliveryCityName
                                 ?? q.DeliveryCountryName
                                 ?? q.DestinationName
                                 ?? q.PortOfDeliveryName
                                 ?? string.Empty;

            worksheet.Cell(row, 1).Value = q.Id.ToString();
            worksheet.Cell(row, 2).Value = q.CreationDate;
            worksheet.Cell(row, 3).Value = q.QuoteNumber;
            worksheet.Cell(row, 4).Value = q.CompanyName ?? q.ShipperName ?? string.Empty;
            worksheet.Cell(row, 5).Value = q.CarrierName ?? string.Empty;
            worksheet.Cell(row, 6).Value = q.StartDate;
            worksheet.Cell(row, 7).Value = q.ExpirationDate;
            worksheet.Cell(row, 8).Value = cargoInfo ?? string.Empty;
            worksheet.Cell(row, 9).Value = loadingPlace;
            worksheet.Cell(row, 10).Value = unloadingPlace;
            worksheet.Cell(row, 11).Value = q.PriceStandard;
            worksheet.Cell(row, 12).Value = q.ManagerName ?? string.Empty;
            worksheet.Cell(row, 13).Value = q.QuoteNumber;
            row++;
        }

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var bytes = stream.ToArray();

        var fileNameBuilder = new StringBuilder("quotes");
        if (!string.IsNullOrWhiteSpace(direction))
        {
            fileNameBuilder.Append('_').Append(direction);
        }
        if (!string.IsNullOrWhiteSpace(mode))
        {
            fileNameBuilder.Append('_').Append(mode.Replace(',', '-'));
        }
        if (!string.IsNullOrWhiteSpace(subType))
        {
            fileNameBuilder.Append('_').Append(subType.Replace(',', '-'));
        }
        fileNameBuilder.Append(".xlsx");

        return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileNameBuilder.ToString());
    }

    [HttpGet("{id:guid}/export")]
    public async Task<IActionResult> ExportSingleToExcel(Guid id, CancellationToken ct)
    {
        var quote = await mediator.Send(new GetQuoteByIdQuery(id), ct);
        if (quote == null) return NotFound();

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Quote");

        var headers = new[]
        {
            "ID",
            "Quote number",
            "Creation date",
            "Client",
            "Manager",
            "Status",
            "Mode",
            "Direction",
            "Sub type",
            "Start date",
            "Expiration date",
            "Cargo information",
            "Loading place",
            "Unloading place",
            "Deal value",
            "Currency",
            "Gross weight (KG)",
            "Volume (CBM)",
            "Chargeable weight (KG)",
            "Number of packages"
        };

        for (var i = 0; i < headers.Length; i++)
        {
            worksheet.Cell(1, i + 1).Value = headers[i];
        }

        var cargoInfo = quote.DescriptionOfGoods;
        if (string.IsNullOrWhiteSpace(cargoInfo))
        {
            if (quote.Quantity1.HasValue && !string.IsNullOrWhiteSpace(quote.PackageType1))
            {
                cargoInfo = $"{quote.Quantity1} x {quote.PackageType1}";
            }
        }

        var loadingPlace = quote.PickupCityName
                           ?? quote.PickupCountryName
                           ?? quote.GatewayName
                           ?? quote.MyPortName
                           ?? string.Empty;

        var unloadingPlace = quote.DeliveryCityName
                             ?? quote.DeliveryCountryName
                             ?? quote.DestinationName
                             ?? quote.PortOfDeliveryName
                             ?? string.Empty;

        var dealValue = quote.PriceStandard;
        var currency = quote.CurrencyCode;

        worksheet.Cell(2, 1).Value = quote.Id.ToString();
        worksheet.Cell(2, 2).Value = quote.QuoteNumber;
        worksheet.Cell(2, 3).Value = quote.CreationDate;
        worksheet.Cell(2, 4).Value = quote.CompanyName ?? quote.ShipperName ?? string.Empty;
        worksheet.Cell(2, 5).Value = quote.ManagerName ?? string.Empty;
        worksheet.Cell(2, 6).Value = quote.QuoteStatus ?? string.Empty;
        worksheet.Cell(2, 7).Value = quote.QuoteTypeMode ?? string.Empty;
        worksheet.Cell(2, 8).Value = quote.QuoteTypeDirection ?? string.Empty;
        worksheet.Cell(2, 9).Value = quote.QuoteTypeSubType ?? string.Empty;
        worksheet.Cell(2, 10).Value = quote.StartDate;
        worksheet.Cell(2, 11).Value = quote.ExpirationDate;
        worksheet.Cell(2, 12).Value = cargoInfo ?? string.Empty;
        worksheet.Cell(2, 13).Value = loadingPlace;
        worksheet.Cell(2, 14).Value = unloadingPlace;
        worksheet.Cell(2, 15).Value = dealValue;
        worksheet.Cell(2, 16).Value = currency ?? string.Empty;
        worksheet.Cell(2, 17).Value = quote.GrossWeightKg;
        worksheet.Cell(2, 18).Value = quote.VolumeCbm;
        worksheet.Cell(2, 19).Value = quote.ChargeableWeightKg;
        worksheet.Cell(2, 20).Value = quote.NumberOfPackages;

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var bytes = stream.ToArray();

        var fileName = $"quote_{quote.QuoteNumber}.xlsx";
        return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }

    /// <summary>Sales funnel: quote counts by stage. Use applyFilters=true with mode/direction/subType to match the quote list; default is all quotes.</summary>
    [HttpGet("funnel")]
    public async Task<ActionResult<QuoteFunnelSummaryDto>> GetFunnel(
        [FromQuery] string? mode,
        [FromQuery] string? direction,
        [FromQuery] string? subType,
        [FromQuery] bool applyFilters = false,
        CancellationToken cancellationToken = default)
        => Ok(await mediator.Send(new GetQuoteFunnelQuery(mode, direction, subType, applyFilters), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<QuoteDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetQuoteByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<QuoteDto>> Create([FromBody] CreateOrUpdateQuoteDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateQuoteCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<QuoteDto>> Update(Guid id, [FromBody] CreateOrUpdateQuoteDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateQuoteCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
    {
        var deleted = await mediator.Send(new DeleteQuoteCommand(id), ct);
        return deleted ? NoContent() : NotFound();
    }
}

