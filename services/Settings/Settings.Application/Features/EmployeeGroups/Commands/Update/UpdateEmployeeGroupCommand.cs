using MediatR;
using Settings.Application.DTOs.EmployeeGroup;

namespace Settings.Application.Features.EmployeeGroups.Commands.Update;

public sealed record UpdateEmployeeGroupCommand(Guid Id, UpdateEmployeeGroupDto Dto) : IRequest<EmployeeGroupDetailDto?>;
