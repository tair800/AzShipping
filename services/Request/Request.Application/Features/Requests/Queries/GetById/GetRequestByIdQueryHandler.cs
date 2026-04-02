using MediatR;
using Request.Application.DTOs.Request;
using Request.Application.Features.Requests;
using Request.Domain.AggregatesModel.RequestAggregate;

namespace Request.Application.Features.Requests.Queries.GetById;

public sealed class GetRequestByIdQueryHandler(
    IRequestRepository repository,
    IRequestTypeRepository typeRepository,
    IRequestDimensionRepository dimensionRepository,
    IRequestVasRepository vasRepository) : IRequestHandler<GetRequestByIdQuery, RequestDto?>
{
    public async Task<RequestDto?> Handle(GetRequestByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        var reqType = await typeRepository.GetByIdAsync(entity.RequestTypeId, cancellationToken);
        var dims = await dimensionRepository.GetByRequestIdAsync(entity.Id, cancellationToken);
        var vasItems = await vasRepository.GetByRequestIdAsync(entity.Id, cancellationToken);
        return RequestMapper.MapToDto(entity, reqType, dims, vasItems);
    }
}
