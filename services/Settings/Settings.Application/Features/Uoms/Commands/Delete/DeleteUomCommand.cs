using MediatR;

namespace Settings.Application.Features.Uoms.Commands.Delete;

public sealed record DeleteUomCommand(Guid Id) : IRequest<bool>;
