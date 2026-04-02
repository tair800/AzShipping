using MediatR;
using Settings.Application.DTOs.Department;

namespace Settings.Application.Features.Departments.Commands.Create;

public sealed record CreateDepartmentCommand(CreateDepartmentDto Dto) : IRequest<DepartmentDto>;
