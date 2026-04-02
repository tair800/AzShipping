using Clients.Application.DTOs.Client;
using MediatR;

namespace Clients.Application.Features.Clients.Commands.Update;

public sealed record UpdateClientCommand(Guid Id, UpdateClientDto Dto) : IRequest<ClientDto?>;
