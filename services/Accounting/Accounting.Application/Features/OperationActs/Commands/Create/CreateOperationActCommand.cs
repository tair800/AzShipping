using Accounting.Application.DTOs.OperationAct;
using MediatR;

namespace Accounting.Application.Features.OperationActs.Commands.Create;

public sealed record CreateOperationActCommand(CreateOperationActDto Dto) : IRequest<OperationActListItemDto>;
