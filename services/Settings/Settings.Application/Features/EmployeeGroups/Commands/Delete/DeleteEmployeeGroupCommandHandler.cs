using MediatR;
using Settings.Domain.AggregatesModel.EmployeeGroupAggregate;

namespace Settings.Application.Features.EmployeeGroups.Commands.Delete;

public sealed class DeleteEmployeeGroupCommandHandler(IEmployeeGroupRepository repository)
    : IRequestHandler<DeleteEmployeeGroupCommand, bool>
{
    public async Task<bool> Handle(DeleteEmployeeGroupCommand request, CancellationToken cancellationToken)
    {
        var e = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (e == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
