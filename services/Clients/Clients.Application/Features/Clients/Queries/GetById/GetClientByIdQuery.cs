using Clients.Application.DTOs.Client;
using MediatR;

namespace Clients.Application.Features.Clients.Queries.GetById;

public sealed record GetClientByIdQuery(Guid Id) : IRequest<ClientDto?>;
