using Accounting.Application.DTOs.VatDefinition;
using Accounting.Application.Features.VatDefinitions;
using Accounting.Domain.AggregatesModel.VatDefinitionAggregate;
using MediatR;

namespace Accounting.Application.Features.VatDefinitions.Queries.GetById;

public sealed class GetVatDefinitionByIdQueryHandler(IVatDefinitionRepository repository)
    : IRequestHandler<GetVatDefinitionByIdQuery, VatDefinitionDto?>
{
    public async Task<VatDefinitionDto?> Handle(GetVatDefinitionByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await repository.GetByIdAsync(request.Id, cancellationToken);
        return e == null ? null : VatDefinitionMapper.ToDto(e);
    }
}
