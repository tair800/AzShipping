using MediatR;
using Settings.Application.DTOs.Numeration;

namespace Settings.Application.Features.Numerations.Queries.GetAll;

public sealed record GetAllNumerationsQuery : IRequest<IReadOnlyList<NumerationDto>>;
