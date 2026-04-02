using Clients.Application.DTOs.Client;
using MediatR;

namespace Clients.Application.Features.Clients.Commands.Create;

public sealed record CreateClientCommand(CreateClientDto Dto) : IRequest<ClientDto>;
