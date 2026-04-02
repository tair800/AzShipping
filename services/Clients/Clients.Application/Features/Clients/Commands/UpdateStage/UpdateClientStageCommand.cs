using Clients.Application.DTOs.Client;
using MediatR;

namespace Clients.Application.Features.Clients.Commands.UpdateStage;

public sealed record UpdateClientStageCommand(Guid ClientId, Guid? ClientStatusId) : IRequest<ClientDto?>;
