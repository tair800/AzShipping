using Accounting.Application.DTOs.VatDefinition;
using MediatR;

namespace Accounting.Application.Features.VatDefinitions.Queries.Legacy;

public record GetVatRateLegacyByIdQuery(Guid Id) : IRequest<VatRateLegacyDto?>;
