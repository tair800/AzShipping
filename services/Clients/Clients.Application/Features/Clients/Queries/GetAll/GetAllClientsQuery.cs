using Clients.Application.DTOs.Client;
using MediatR;

namespace Clients.Application.Features.Clients.Queries.GetAll;

public sealed record GetAllClientsQuery : IRequest<IReadOnlyList<ClientDto>>;
