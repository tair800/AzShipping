using MediatR;

namespace Carrier.Application.Features.Carriers.Commands.Delete;

public sealed record DeleteCarrierCommand(Guid Id) : IRequest<bool>;
