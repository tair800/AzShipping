using Clients.Application.DTOs.Client;
using Clients.Application.Features.Clients;
using Clients.Application.Services;
using Clients.Domain.AggregatesModel.ClientAggregate;
using MediatR;

namespace Clients.Application.Features.Clients.Queries.GetById;

public sealed class GetClientByIdQueryHandler(IClientRepository repository, ISettingsReferenceDataClient settingsReferenceData)
    : IRequestHandler<GetClientByIdQuery, ClientDto?>
{
    public async Task<ClientDto?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        var mapped = ClientMapper.MapToDto(entity);
        return await ClientResponseEnricher.EnrichFromSettingsAsync(mapped, settingsReferenceData, cancellationToken);
    }
}
