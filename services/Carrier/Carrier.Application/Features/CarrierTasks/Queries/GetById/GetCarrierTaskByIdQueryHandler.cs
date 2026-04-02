using Carrier.Application.DTOs.CarrierTask;
using Carrier.Application.Features.CarrierTasks;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.CarrierTasks.Queries.GetById;

public sealed class GetCarrierTaskByIdQueryHandler(ITaskRepository repository)
    : IRequestHandler<GetCarrierTaskByIdQuery, CarrierTaskDto?>
{
    public async Task<CarrierTaskDto?> Handle(GetCarrierTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : CarrierTaskMapper.MapToDto(entity);
    }
}
