using MediatR;

namespace Settings.Application.Features.CarrierTypes.Commands.Delete;

public sealed record DeleteCarrierTypeCommand(Guid Id) : IRequest<bool>;
