using MediatR;
using Request.Application.DTOs.Request;
using Request.Application.Features.Requests;
using Request.Domain.AggregatesModel.RequestAggregate;

namespace Request.Application.Features.Requests.Queries.GetRequestTypeById;

public sealed class GetRequestTypeByIdQueryHandler(IRequestTypeRepository repository)
    : IRequestHandler<GetRequestTypeByIdQuery, RequestTypeDto?>
{
    public async Task<RequestTypeDto?> Handle(GetRequestTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : RequestMapper.MapTypeToDto(entity);
    }
}
