using Accounting.Application.DTOs.VatDefinition;
using MediatR;

namespace Accounting.Application.Features.VatDefinitions.Queries.Legacy;

public record GetAllVatRatesLegacyQuery : IRequest<IReadOnlyList<VatRateLegacyDto>>;
