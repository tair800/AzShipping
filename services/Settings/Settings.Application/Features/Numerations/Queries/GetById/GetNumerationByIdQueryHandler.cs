using MediatR;
using Settings.Application.DTOs.Numeration;
using Settings.Domain.AggregatesModel.NumerationAggregate;

namespace Settings.Application.Features.Numerations.Queries.GetById;

public sealed class GetNumerationByIdQueryHandler(INumerationRepository repository)
    : IRequestHandler<GetNumerationByIdQuery, NumerationDto?>
{
    public async Task<NumerationDto?> Handle(GetNumerationByIdQuery request, CancellationToken ct)
    {
        var e = await repository.GetByIdAsync(request.Id, ct);
        return e == null ? null : MapToDto(e);
    }

    private static NumerationDto MapToDto(Numeration e) => new(
        e.Id, e.Name, e.NumerationForCode,
        e.CompanyId, e.Company?.Name, e.DepartmentId, e.Department?.Name,
        e.EmployeeId, e.ClientId, e.ElementCode, e.DocumentTypeCode,
        e.NumberOfDigits, e.CurrentIndex, e.Formula, e.IsSystemic,
        e.CreatedAt, e.UpdatedAt);
}
