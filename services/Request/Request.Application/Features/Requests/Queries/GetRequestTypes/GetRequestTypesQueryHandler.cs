using MediatR;
using Request.Application.DTOs.Request;
using Request.Application.Features.Requests;
using Request.Domain.AggregatesModel.RequestAggregate;

namespace Request.Application.Features.Requests.Queries.GetRequestTypes;

public sealed class GetRequestTypesQueryHandler(IRequestTypeRepository repository)
    : IRequestHandler<GetRequestTypesQuery, IReadOnlyList<RequestTypeDto>>
{
    public async Task<IReadOnlyList<RequestTypeDto>> Handle(GetRequestTypesQuery request, CancellationToken cancellationToken)
    {
        var types = request.IncludeInactive
            ? await repository.GetAllAsync(cancellationToken)
            : await repository.GetAllActiveAsync(cancellationToken);
        return types.Select(RequestMapper.MapTypeToDto).ToList();
    }
}
