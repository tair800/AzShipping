using MediatR;
using Settings.Application.DTOs.Numeration;
using Settings.Domain.AggregatesModel.NumerationAggregate;

namespace Settings.Application.Features.Numerations.Queries.GetAll;

public sealed class GetAllNumerationsQueryHandler(INumerationRepository repository)
    : IRequestHandler<GetAllNumerationsQuery, IReadOnlyList<NumerationDto>>
{
    public async Task<IReadOnlyList<NumerationDto>> Handle(GetAllNumerationsQuery request, CancellationToken ct)
    {
        var list = await repository.GetAllAsync(ct);
        return list.Select(MapToDto).ToList();
    }

    private static NumerationDto MapToDto(Numeration e) => new(
        e.Id, e.Name, e.NumerationForCode,
        e.CompanyId, e.Company?.Name, e.DepartmentId, e.Department?.Name,
        e.EmployeeId, e.ClientId, e.ElementCode, e.DocumentTypeCode,
        e.NumberOfDigits, e.CurrentIndex, e.Formula, e.IsSystemic,
        e.CreatedAt, e.UpdatedAt);
}
