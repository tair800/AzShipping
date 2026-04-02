using Clients.Application.DTOs.Client;
using Clients.Application.Features.Clients;
using Clients.Application.Services;
using Clients.Domain.AggregatesModel.ClientAggregate;
using MediatR;

namespace Clients.Application.Features.Clients.Queries.GetAll;

public sealed class GetAllClientsQueryHandler(IClientRepository repository, ISettingsReferenceDataClient settingsReferenceData)
    : IRequestHandler<GetAllClientsQuery, IReadOnlyList<ClientDto>>
{
    public async Task<IReadOnlyList<ClientDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        var mapped = list.Select(ClientMapper.MapToDto).ToList();
        return await ClientResponseEnricher.EnrichFromSettingsAsync(mapped, settingsReferenceData, cancellationToken);
    }
}
