using MediatR;
using Settings.Application.DTOs.EmployeeGroup;

namespace Settings.Application.Features.EmployeeGroups.Commands.Clone;

public sealed record CloneEmployeeGroupCommand(Guid SourceId) : IRequest<EmployeeGroupDetailDto?>;
