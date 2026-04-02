using MediatR;

namespace General.Application.Features.Incoterms.Commands.Delete;

public record DeleteIncotermCommand(Guid Id, bool SoftDelete = false) : IRequest<bool>;
