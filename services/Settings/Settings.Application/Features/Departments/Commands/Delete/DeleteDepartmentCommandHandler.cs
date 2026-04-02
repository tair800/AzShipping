using MediatR;
using Settings.Domain.AggregatesModel.DepartmentAggregate;

namespace Settings.Application.Features.Departments.Commands.Delete;

public sealed class DeleteDepartmentCommandHandler(IDepartmentRepository repository) : IRequestHandler<DeleteDepartmentCommand, bool>
{
    public async Task<bool> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
