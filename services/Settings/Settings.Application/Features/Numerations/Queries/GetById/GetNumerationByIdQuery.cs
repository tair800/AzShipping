using MediatR;
using Settings.Application.DTOs.Numeration;

namespace Settings.Application.Features.Numerations.Queries.GetById;

public sealed record GetNumerationByIdQuery(Guid Id) : IRequest<NumerationDto?>;
