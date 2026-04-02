using MediatR;
using Settings.Domain.AggregatesModel.EmailAccountAggregate;

namespace Settings.Application.Features.EmailSettings.Commands.Delete;

public sealed class DeleteEmailSettingCommandHandler(IEmailAccountSettingRepository repository)
    : IRequestHandler<DeleteEmailSettingCommand, bool>
{
    public async Task<bool> Handle(DeleteEmailSettingCommand request, CancellationToken cancellationToken)
    {
        var e = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (e == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
