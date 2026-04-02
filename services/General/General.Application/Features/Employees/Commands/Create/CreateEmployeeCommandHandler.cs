using General.Application.DTOs.Employee;
using General.Application.Services;
using General.Domain.AggregatesModel.EmployeeAggregate;
using MediatR;

namespace General.Application.Features.Employees.Commands.Create;

public class CreateEmployeeCommandHandler(
    IEmployeeRepository repository,
    IActionLogClient actionLogClient,
    ISettingsCatalogLookup catalogLookup)
    : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
{
    public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        if (dto.UserId <= 0)
            throw new InvalidOperationException("UserId must be a positive Identity user id.");

        if (await repository.ExistsByUserIdAsync(dto.UserId, null, cancellationToken))
            throw new InvalidOperationException($"An employee already exists for UserId {dto.UserId}.");

        var now = DateTime.UtcNow;
        var entity = new Employee
        {
            Id = Guid.NewGuid(),
            UserId = dto.UserId,
            FullName = string.IsNullOrWhiteSpace(dto.FullName) ? null : dto.FullName.Trim(),
            Username = string.IsNullOrWhiteSpace(dto.Username) ? null : dto.Username.Trim(),
            DepartmentId = dto.DepartmentId,
            WorkerPostId = dto.WorkerPostId,
            Email = string.IsNullOrWhiteSpace(dto.Email) ? null : dto.Email.Trim(),
            Phone = string.IsNullOrWhiteSpace(dto.Phone) ? null : dto.Phone.Trim(),
            ContractNumber = string.IsNullOrWhiteSpace(dto.ContractNumber) ? null : dto.ContractNumber.Trim(),
            Address = string.IsNullOrWhiteSpace(dto.Address) ? null : dto.Address.Trim(),
            ProfileImageUrl = string.IsNullOrWhiteSpace(dto.ProfileImageUrl) ? null : dto.ProfileImageUrl.Trim(),
            CreatedAtUtc = now
        };

        await repository.AddAsync(entity, cancellationToken);
        var created = await repository.GetByIdAsync(entity.Id, cancellationToken);
        await actionLogClient.LogAsync("Employee created", $"employee: {entity.FullName ?? entity.Username ?? entity.Id.ToString()} • id: {entity.Id}", null, null, cancellationToken);
        return await catalogLookup.ToEmployeeDtoAsync(created!, cancellationToken);
    }
}
