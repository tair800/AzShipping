using MediatR;

namespace Carrier.Application.Features.CarrierTasks.Commands.Delete;

public record DeleteCarrierTaskCommand(Guid Id) : IRequest<bool>;
