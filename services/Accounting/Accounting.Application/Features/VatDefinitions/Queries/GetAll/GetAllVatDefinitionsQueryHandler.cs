using Accounting.Application.DTOs.VatDefinition;
using Accounting.Application.Features.VatDefinitions;
using Accounting.Domain.AggregatesModel.VatDefinitionAggregate;
using MediatR;

namespace Accounting.Application.Features.VatDefinitions.Queries.GetAll;

public sealed class GetAllVatDefinitionsQueryHandler(IVatDefinitionRepository repository)
    : IRequestHandler<GetAllVatDefinitionsQuery, IReadOnlyList<VatDefinitionDto>>
{
    public async Task<IReadOnlyList<VatDefinitionDto>> Handle(GetAllVatDefinitionsQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(VatDefinitionMapper.ToDto).ToList();
    }
}
