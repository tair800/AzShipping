using MediatR;
using Settings.Application.DTOs.Department;

namespace Settings.Application.Features.Departments.Commands.Update;

public sealed record UpdateDepartmentCommand(Guid Id, UpdateDepartmentDto Dto) : IRequest<DepartmentDto?>;
