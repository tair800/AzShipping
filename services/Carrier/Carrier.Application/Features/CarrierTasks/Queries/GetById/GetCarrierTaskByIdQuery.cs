using Carrier.Application.DTOs.CarrierTask;
using MediatR;

namespace Carrier.Application.Features.CarrierTasks.Queries.GetById;

public sealed record GetCarrierTaskByIdQuery(Guid Id) : IRequest<CarrierTaskDto?>;
