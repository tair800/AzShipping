using Accounting.Application.DTOs.VatDefinition;
using Accounting.Application.Features.VatDefinitions;
using Accounting.Domain.AggregatesModel.VatDefinitionAggregate;
using MediatR;

namespace Accounting.Application.Features.VatDefinitions.Queries.Legacy;

public sealed class GetVatRateLegacyByIdQueryHandler(IVatDefinitionRepository repository)
    : IRequestHandler<GetVatRateLegacyByIdQuery, VatRateLegacyDto?>
{
    public async Task<VatRateLegacyDto?> Handle(GetVatRateLegacyByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await repository.GetByIdAsync(request.Id, cancellationToken);
        return e == null ? null : VatDefinitionMapper.ToLegacy(e);
    }
}
