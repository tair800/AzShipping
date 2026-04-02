using General.Application.DTOs.Vas;
using MediatR;

namespace General.Application.Features.Vas.Queries.GetById;

public record GetVasByIdQuery(Guid Id) : IRequest<VasDto?>;
