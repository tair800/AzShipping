using MediatR;
using Settings.Domain.AggregatesModel.MeetingTypeAggregate;

namespace Settings.Application.Features.MeetingTypes.Commands.Delete;

public sealed class DeleteMeetingTypeCommandHandler(IMeetingTypeRepository repository) : IRequestHandler<DeleteMeetingTypeCommand, bool>
{
    public async Task<bool> Handle(DeleteMeetingTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
