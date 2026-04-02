using General.Application.DTOs.Employee;
using MediatR;

namespace General.Application.Features.Employees.Commands.CreateNote;

public record CreateEmployeeNoteCommand(Guid EmployeeId, CreateEmployeeNoteDto Dto) : IRequest<EmployeeNoteDto?>;
