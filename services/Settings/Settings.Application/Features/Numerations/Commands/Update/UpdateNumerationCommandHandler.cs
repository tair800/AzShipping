using MediatR;
using Settings.Application.DTOs.Numeration;
using Settings.Application.Services;
using Settings.Domain.AggregatesModel.NumerationAggregate;

namespace Settings.Application.Features.Numerations.Commands.Update;

public sealed class UpdateNumerationCommandHandler(INumerationRepository repository, IInternalActionLogService actionLog)
    : IRequestHandler<UpdateNumerationCommand, NumerationDto?>
{
    public async Task<NumerationDto?> Handle(UpdateNumerationCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return null;
        var d = request.Dto;
        entity.Name = d.Name;
        entity.NumerationForCode = d.NumerationForCode;
        entity.CompanyId = d.CompanyId;
        entity.DepartmentId = d.DepartmentId;
        entity.EmployeeId = d.EmployeeId;
        entity.ClientId = d.ClientId;
        entity.ElementCode = d.ElementCode;
        entity.DocumentTypeCode = d.DocumentTypeCode;
        entity.NumberOfDigits = d.NumberOfDigits;
        entity.CurrentIndex = d.CurrentIndex;
        entity.Formula = d.Formula;
        entity.IsSystemic = d.IsSystemic;
        await repository.UpdateAsync(entity, ct);
        var loaded = await repository.GetByIdAsync(entity.Id, ct);
        var result = loaded == null ? null : MapToDto(loaded);
        await actionLog.LogAsync("Numeration updated", $"numeration: {entity.Name} • id: {entity.Id}", entity.EmployeeId, null, ct);
        return result;
    }

    private static NumerationDto MapToDto(Numeration e) => new(
        e.Id, e.Name, e.NumerationForCode,
        e.CompanyId, e.Company?.Name, e.DepartmentId, e.Department?.Name,
        e.EmployeeId, e.ClientId, e.ElementCode, e.DocumentTypeCode,
        e.NumberOfDigits, e.CurrentIndex, e.Formula, e.IsSystemic,
        e.CreatedAt, e.UpdatedAt);
}
