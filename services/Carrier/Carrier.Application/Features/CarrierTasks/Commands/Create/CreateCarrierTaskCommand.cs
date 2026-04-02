using Carrier.Application.DTOs.CarrierTask;
using MediatR;

namespace Carrier.Application.Features.CarrierTasks.Commands.Create;

public record CreateCarrierTaskCommand(Guid CarrierId, CreateCarrierTaskDto Dto) : IRequest<CarrierTaskDto>;  // CarrierId for route, Dto has ProjectId
