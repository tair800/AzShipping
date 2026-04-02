using Accounting.Application.DTOs.OperationAct;
using MediatR;

namespace Accounting.Application.Features.OperationActs.Queries.GetAll;

public sealed record GetAllOperationActsQuery : IRequest<IReadOnlyList<OperationActListItemDto>>;
