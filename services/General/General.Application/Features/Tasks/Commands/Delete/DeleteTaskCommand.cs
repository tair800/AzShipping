using MediatR;

namespace General.Application.Features.Tasks.Commands.Delete;

public record DeleteTaskCommand(Guid Id) : IRequest<bool>;
