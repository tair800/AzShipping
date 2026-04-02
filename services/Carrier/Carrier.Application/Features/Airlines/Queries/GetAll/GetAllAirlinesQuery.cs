using Carrier.Application.DTOs.Airline;
using MediatR;

namespace Carrier.Application.Features.Airlines.Queries.GetAll;

public record GetAllAirlinesQuery(bool? IsActive) : IRequest<IReadOnlyList<AirlineDto>>;
