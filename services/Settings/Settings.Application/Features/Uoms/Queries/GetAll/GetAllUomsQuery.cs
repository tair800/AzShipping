using MediatR;
using Settings.Application.DTOs.Uom;

namespace Settings.Application.Features.Uoms.Queries.GetAll;

public sealed record GetAllUomsQuery : IRequest<IReadOnlyList<UomDto>>;
