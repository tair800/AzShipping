using MediatR;
using Settings.Application.DTOs.Numeration;
using Settings.Application.Services;
using Settings.Domain.AggregatesModel.NumerationAggregate;

namespace Settings.Application.Features.Numerations.Commands.Create;

public sealed class CreateNumerationCommandHandler(INumerationRepository repository, IInternalActionLogService actionLog)
    : IRequestHandler<CreateNumerationCommand, NumerationDto>
{
    public async Task<NumerationDto> Handle(CreateNumerationCommand request, CancellationToken ct)
    {
        var d = request.Dto;
        var entity = new Numeration
        {
            Id = Guid.NewGuid(),
            Name = d.Name,
            NumerationForCode = d.NumerationForCode,
            CompanyId = d.CompanyId,
            DepartmentId = d.DepartmentId,
            EmployeeId = d.EmployeeId,
            ClientId = d.ClientId,
            ElementCode = d.ElementCode,
            DocumentTypeCode = d.DocumentTypeCode,
            NumberOfDigits = d.NumberOfDigits,
            CurrentIndex = d.CurrentIndex,
            Formula = d.Formula,
            IsSystemic = d.IsSystemic,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, ct);
        var loaded = await repository.GetByIdAsync(entity.Id, ct);
        var result = MapToDto(loaded!);
        await actionLog.LogAsync("Numeration created", $"numeration: {entity.Name} • id: {entity.Id}", entity.EmployeeId, null, ct);
        return result;
    }

    private static NumerationDto MapToDto(Numeration e) => new(
        e.Id, e.Name, e.NumerationForCode,
        e.CompanyId, e.Company?.Name, e.DepartmentId, e.Department?.Name,
        e.EmployeeId, e.ClientId, e.ElementCode, e.DocumentTypeCode,
        e.NumberOfDigits, e.CurrentIndex, e.Formula, e.IsSystemic,
        e.CreatedAt, e.UpdatedAt);
}
