using MediatR;

namespace Settings.Application.Features.ActionLogs.Commands.Add;

public sealed record AddActionLogCommand(AddActionLogDto Dto) : IRequest<long>;
