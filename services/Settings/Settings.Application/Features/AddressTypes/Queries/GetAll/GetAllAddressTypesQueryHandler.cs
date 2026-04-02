using MediatR;
using Settings.Application.DTOs.AddressType;
using Settings.Domain.AggregatesModel.AddressTypeAggregate;

namespace Settings.Application.Features.AddressTypes.Queries.GetAll;

public sealed class GetAllAddressTypesQueryHandler(IAddressTypeRepository repository)
    : IRequestHandler<GetAllAddressTypesQuery, IReadOnlyList<AddressTypeDto>>
{
    public async Task<IReadOnlyList<AddressTypeDto>> Handle(GetAllAddressTypesQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(e => new AddressTypeDto(e.Id, e.Code, e.Name, e.Description)).ToList();
    }
}
