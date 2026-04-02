using Carrier.Application.DTOs.CarrierTask;
using MediatR;

namespace Carrier.Application.Features.CarrierTasks.Commands.Update;

public record UpdateCarrierTaskCommand(Guid Id, UpdateCarrierTaskDto Dto) : IRequest<CarrierTaskDto?>;
