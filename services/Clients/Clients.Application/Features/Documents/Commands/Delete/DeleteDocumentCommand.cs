using MediatR;

namespace Clients.Application.Features.Documents.Commands.Delete;

public sealed record DeleteDocumentCommand(Guid Id) : IRequest<bool>;
