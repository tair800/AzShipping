using MediatR;
using Settings.Domain.AggregatesModel.MeetingResultAggregate;

namespace Settings.Application.Features.MeetingResults.Commands.Delete;

public sealed class DeleteMeetingResultCommandHandler(IMeetingResultRepository repository) : IRequestHandler<DeleteMeetingResultCommand, bool>
{
    public async Task<bool> Handle(DeleteMeetingResultCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
