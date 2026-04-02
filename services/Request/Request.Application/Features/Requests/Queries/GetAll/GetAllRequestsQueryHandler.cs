using MediatR;
using Request.Application.DTOs.Request;
using Request.Application.Features.Requests;
using Request.Domain.AggregatesModel.RequestAggregate;

namespace Request.Application.Features.Requests.Queries.GetAll;

public sealed class GetAllRequestsQueryHandler(
    IRequestRepository repository,
    IRequestTypeRepository typeRepository,
    IRequestDimensionRepository dimensionRepository,
    IRequestVasRepository vasRepository) : IRequestHandler<GetAllRequestsQuery, IReadOnlyList<RequestDto>>
{
    public async Task<IReadOnlyList<RequestDto>> Handle(GetAllRequestsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(request.TypeCode, request.Mode, request.Direction, request.SubType, cancellationToken);
        var result = new List<RequestDto>();
        foreach (var entity in entities)
        {
            var reqType = await typeRepository.GetByIdAsync(entity.RequestTypeId, cancellationToken);
            var dims = await dimensionRepository.GetByRequestIdAsync(entity.Id, cancellationToken);
            var vasItems = await vasRepository.GetByRequestIdAsync(entity.Id, cancellationToken);
            result.Add(RequestMapper.MapToDto(entity, reqType, dims, vasItems));
        }
        return result;
    }
}
