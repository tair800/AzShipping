using MediatR;

namespace Settings.Application.Features.ActionLogs.Queries.GetActions;

public sealed record GetActionLogActionsQuery : IRequest<IReadOnlyList<string>>;
