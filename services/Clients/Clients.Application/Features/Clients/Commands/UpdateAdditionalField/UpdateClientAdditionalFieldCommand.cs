using Clients.Application.DTOs.Client;
using MediatR;

namespace Clients.Application.Features.Clients.Commands.UpdateAdditionalField;

public sealed record UpdateClientAdditionalFieldCommand(Guid ClientId, string? Comment) : IRequest<ClientDto?>;
