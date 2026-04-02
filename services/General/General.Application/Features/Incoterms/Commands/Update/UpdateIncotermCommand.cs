using General.Application.DTOs.Incoterm;
using MediatR;

namespace General.Application.Features.Incoterms.Commands.Update;

public record UpdateIncotermCommand(Guid Id, UpdateIncotermDto Dto) : IRequest<IncotermDto?>;
