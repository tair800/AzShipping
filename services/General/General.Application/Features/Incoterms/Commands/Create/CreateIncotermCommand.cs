using General.Application.DTOs.Incoterm;
using MediatR;

namespace General.Application.Features.Incoterms.Commands.Create;

public record CreateIncotermCommand(CreateIncotermDto Dto) : IRequest<IncotermDto>;
