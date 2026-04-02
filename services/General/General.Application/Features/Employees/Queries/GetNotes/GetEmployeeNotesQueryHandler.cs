using General.Application.DTOs.Employee;
using General.Domain.AggregatesModel.EmployeeAggregate;
using MediatR;

namespace General.Application.Features.Employees.Queries.GetNotes;

public class GetEmployeeNotesQueryHandler(IEmployeeRepository employees, IEmployeeNoteRepository notes)
    : IRequestHandler<GetEmployeeNotesQuery, IReadOnlyList<EmployeeNoteDto>?>
{
    public async Task<IReadOnlyList<EmployeeNoteDto>?> Handle(GetEmployeeNotesQuery request, CancellationToken cancellationToken)
    {
        var emp = await employees.GetByIdAsync(request.EmployeeId, cancellationToken);
        if (emp == null)
            return null;

        var list = await notes.ListByEmployeeIdAsync(request.EmployeeId, cancellationToken);
        return list.Select(n => new EmployeeNoteDto
        {
            Id = n.Id,
            EmployeeId = n.EmployeeId,
            Content = n.Content,
            NoteDate = n.NoteDate,
            CreatedAtUtc = n.CreatedAtUtc
        }).ToList();
    }
}
