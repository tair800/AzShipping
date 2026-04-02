using Carrier.Application.DTOs.CarrierTask;
using Carrier.Application.Features.CarrierTasks;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.CarrierTasks.Queries.GetByCarrierId;

public sealed class GetCarrierTasksQueryHandler(ITaskRepository repository)
    : IRequestHandler<GetCarrierTasksQuery, IReadOnlyList<CarrierTaskDto>>
{
    public async Task<IReadOnlyList<CarrierTaskDto>> Handle(GetCarrierTasksQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetByCarrierIdAsync(request.CarrierId, cancellationToken);
        return list.Select(CarrierTaskMapper.MapToDto).ToList();
    }
}
