using Clients.Application.DTOs.Client;
using Clients.Application.Features.Clients;
using Clients.Application.Services;
using Clients.Domain.AggregatesModel.ClientAggregate;
using MediatR;

namespace Clients.Application.Features.Clients.Commands.UpdateAdditionalField;

public sealed class UpdateClientAdditionalFieldCommandHandler(IClientRepository repository, ISettingsReferenceDataClient settingsReferenceData)
    : IRequestHandler<UpdateClientAdditionalFieldCommand, ClientDto?>
{
    public async Task<ClientDto?> Handle(UpdateClientAdditionalFieldCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.ClientId, cancellationToken);
        if (entity == null) return null;

        entity.Comment = request.Comment;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, cancellationToken);

        var updated = await repository.GetByIdAsync(entity.Id, cancellationToken);
        if (updated == null) return null;
        var mapped = ClientMapper.MapToDto(updated);
        return await ClientResponseEnricher.EnrichFromSettingsAsync(mapped, settingsReferenceData, cancellationToken);
    }
}
