using MediatR;

namespace Settings.Application.Features.MessageLogs.Commands.Add;

public sealed record AddMessageLogCommand(AddMessageLogDto Dto) : IRequest<long>;
