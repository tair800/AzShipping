using MediatR;
using Settings.Application.DTOs.EmployeeGroup;

namespace Settings.Application.Features.EmployeeGroups.Commands.Create;

public sealed record CreateEmployeeGroupCommand(CreateEmployeeGroupDto Dto) : IRequest<EmployeeGroupDetailDto>;
