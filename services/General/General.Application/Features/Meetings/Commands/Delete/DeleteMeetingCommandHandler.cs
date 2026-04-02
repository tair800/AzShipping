using General.Application.Services;
using General.Domain.AggregatesModel.MeetingAggregate;
using MediatR;

namespace General.Application.Features.Meetings.Commands.Delete;

public class DeleteMeetingCommandHandler(IMeetingRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<DeleteMeetingCommand, bool>
{
    public async Task<bool> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;

        var name = entity.Name;
        var id = entity.Id;

        await repository.DeleteAsync(request.Id, cancellationToken);

        await actionLogClient.LogAsync("Meeting deleted", $"meeting: {name} • id: {id}", null, null, cancellationToken);
        return true;
    }
}
