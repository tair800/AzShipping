using General.Application.DTOs.Employee;
using General.Application.Services;
using General.Domain.AggregatesModel.EmployeeAggregate;
using MediatR;

namespace General.Application.Features.Employees.Commands.CreateNote;

public class CreateEmployeeNoteCommandHandler(
    IEmployeeRepository employees,
    IEmployeeNoteRepository notes,
    IActionLogClient actionLogClient)
    : IRequestHandler<CreateEmployeeNoteCommand, EmployeeNoteDto?>
{
    public async Task<EmployeeNoteDto?> Handle(CreateEmployeeNoteCommand request, CancellationToken cancellationToken)
    {
        var emp = await employees.GetByIdAsync(request.EmployeeId, cancellationToken);
        if (emp == null)
            return null;

        var text = request.Dto.Content?.Trim() ?? "";
        if (text.Length == 0)
            throw new InvalidOperationException("Note content is required.");
        if (text.Length > 4000)
            throw new InvalidOperationException("Note content must be at most 4000 characters.");

        var now = DateTime.UtcNow;
        var entity = new EmployeeNote
        {
            Id = Guid.NewGuid(),
            EmployeeId = request.EmployeeId,
            Content = text,
            NoteDate = DateOnly.FromDateTime(now),
            CreatedAtUtc = now
        };

        await notes.AddAsync(entity, cancellationToken);
        await actionLogClient.LogAsync("Employee note created", $"employee id: {request.EmployeeId} • note: {entity.Id}", null, null, cancellationToken);

        return new EmployeeNoteDto
        {
            Id = entity.Id,
            EmployeeId = entity.EmployeeId,
            Content = entity.Content,
            NoteDate = entity.NoteDate,
            CreatedAtUtc = entity.CreatedAtUtc
        };
    }
}
