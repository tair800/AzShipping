using General.Application.DTOs.Incoterm;
using MediatR;

namespace General.Application.Features.Incoterms.Queries.GetAll;

public record GetAllIncotermsQuery(bool? IsActive, bool? IsDeleted) : IRequest<IReadOnlyList<IncotermDto>>;
