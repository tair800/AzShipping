using General.Application.DTOs.Incoterm;
using MediatR;

namespace General.Application.Features.Incoterms.Queries.GetById;

public record GetIncotermByIdQuery(Guid Id) : IRequest<IncotermDto?>;
