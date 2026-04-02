using MediatR;

namespace General.Application.Features.Vas.Commands.Delete;

public record DeleteVasCommand(Guid Id, bool SoftDelete = false) : IRequest<bool>;
