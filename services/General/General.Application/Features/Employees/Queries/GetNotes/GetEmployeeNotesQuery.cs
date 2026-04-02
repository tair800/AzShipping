using General.Application.DTOs.Employee;
using MediatR;

namespace General.Application.Features.Employees.Queries.GetNotes;

public record GetEmployeeNotesQuery(Guid EmployeeId) : IRequest<IReadOnlyList<EmployeeNoteDto>?>;
