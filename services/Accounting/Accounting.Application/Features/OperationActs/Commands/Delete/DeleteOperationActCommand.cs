using MediatR;

namespace Accounting.Application.Features.OperationActs.Commands.Delete;

public sealed record DeleteOperationActCommand(long Id) : IRequest<bool>;
