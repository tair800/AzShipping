using General.Application.DTOs.Employee;
using General.Application.Services;
using General.Domain.AggregatesModel.EmployeeAggregate;
using MediatR;

namespace General.Application.Features.Employees.Commands.Update;

public class UpdateEmployeeCommandHandler(
    IEmployeeRepository repository,
    IActionLogClient actionLogClient,
    ISettingsCatalogLookup catalogLookup)
    : IRequestHandler<UpdateEmployeeCommand, EmployeeDto?>
{
    public async Task<EmployeeDto?> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        if (dto.UserId <= 0)
            throw new InvalidOperationException("UserId must be a positive Identity user id.");

        var entity = await repository.GetTrackedByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;

        if (await repository.ExistsByUserIdAsync(dto.UserId, request.Id, cancellationToken))
            throw new InvalidOperationException($"Another employee already uses UserId {dto.UserId}.");

        entity.UserId = dto.UserId;
        entity.FullName = string.IsNullOrWhiteSpace(dto.FullName) ? null : dto.FullName.Trim();
        entity.Username = string.IsNullOrWhiteSpace(dto.Username) ? null : dto.Username.Trim();
        entity.DepartmentId = dto.DepartmentId;
        entity.WorkerPostId = dto.WorkerPostId;
        entity.Email = string.IsNullOrWhiteSpace(dto.Email) ? null : dto.Email.Trim();
        entity.Phone = string.IsNullOrWhiteSpace(dto.Phone) ? null : dto.Phone.Trim();
        entity.ContractNumber = string.IsNullOrWhiteSpace(dto.ContractNumber) ? null : dto.ContractNumber.Trim();
        entity.Address = string.IsNullOrWhiteSpace(dto.Address) ? null : dto.Address.Trim();
        entity.ProfileImageUrl = string.IsNullOrWhiteSpace(dto.ProfileImageUrl) ? null : dto.ProfileImageUrl.Trim();
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await repository.UpdateAsync(entity, cancellationToken);
        var updated = await repository.GetByIdAsync(entity.Id, cancellationToken);
        await actionLogClient.LogAsync("Employee updated", $"employee id: {entity.Id}", null, null, cancellationToken);
        return await catalogLookup.ToEmployeeDtoAsync(updated!, cancellationToken);
    }
}
