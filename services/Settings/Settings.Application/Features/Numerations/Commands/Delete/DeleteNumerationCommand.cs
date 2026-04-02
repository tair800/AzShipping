using MediatR;

namespace Settings.Application.Features.Numerations.Commands.Delete;

public sealed record DeleteNumerationCommand(Guid Id) : IRequest<bool>;
