using General.Application.DTOs.Vas;
using MediatR;

namespace General.Application.Features.Vas.Queries.GetAll;

public record GetAllVasQuery(bool? IsActive, bool? IsDeleted) : IRequest<IReadOnlyList<VasDto>>;
