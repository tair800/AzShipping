using MediatR;
using Settings.Application.DTOs.Uom;

namespace Settings.Application.Features.Uoms.Queries.GetById;

public sealed record GetUomByIdQuery(Guid Id) : IRequest<UomDto?>;
