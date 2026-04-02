using MediatR;
using Settings.Domain.AggregatesModel.MeetingStatusAggregate;

namespace Settings.Application.Features.MeetingStatuses.Commands.Delete;

public sealed class DeleteMeetingStatusCommandHandler(IMeetingStatusRepository repository) : IRequestHandler<DeleteMeetingStatusCommand, bool>
{
    public async Task<bool> Handle(DeleteMeetingStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
