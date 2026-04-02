using General.Application.Services;
using General.Domain.AggregatesModel.EmployeeAggregate;
using MediatR;

namespace General.Application.Features.Employees.Commands.Delete;

public class DeleteEmployeeCommandHandler(IEmployeeRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<DeleteEmployeeCommand, bool>
{
    public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return false;

        await repository.DeleteAsync(request.Id, cancellationToken);
        await actionLogClient.LogAsync("Employee deleted", $"employee id: {request.Id}", null, null, cancellationToken);
        return true;
    }
}
