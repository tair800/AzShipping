using Carrier.Application.DTOs.CarrierTask;
using MediatR;

namespace Carrier.Application.Features.CarrierTasks.Queries.GetByCarrierId;

public sealed record GetCarrierTasksQuery(Guid CarrierId) : IRequest<IReadOnlyList<CarrierTaskDto>>;
