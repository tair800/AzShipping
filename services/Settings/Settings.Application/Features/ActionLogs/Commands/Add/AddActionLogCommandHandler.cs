using MediatR;
using Settings.Domain.AggregatesModel.ActionLogAggregate;

namespace Settings.Application.Features.ActionLogs.Commands.Add;

public sealed class AddActionLogCommandHandler(IActionLogRepository repository)
    : IRequestHandler<AddActionLogCommand, long>
{
    public async Task<long> Handle(AddActionLogCommand request, CancellationToken ct)
    {
        var dto = request.Dto;
        var entity = new ActionLog
        {
            CreatedAt = DateTime.UtcNow,
            Action = dto.Action,
            Data = dto.Data,
            SessionId = dto.SessionId,
            IpAddress = dto.IpAddress,
            Location = dto.Location,
            Browser = dto.Browser,
            EmployeeId = dto.EmployeeId,
            EmployeeName = dto.EmployeeName
        };
        await repository.AddAsync(entity, ct);
        return entity.Id;
    }
}
