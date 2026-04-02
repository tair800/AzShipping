using MediatR;
using Settings.Application.DTOs.RequestSource;
using Settings.Domain.AggregatesModel.RequestSourceAggregate;

namespace Settings.Application.Features.RequestSources.Queries.GetAll;

public sealed class GetAllRequestSourcesQueryHandler(IRequestSourceRepository repository) : IRequestHandler<GetAllRequestSourcesQuery, IReadOnlyList<RequestSourceDto>>
{
    public async Task<IReadOnlyList<RequestSourceDto>> Handle(GetAllRequestSourcesQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(e => new RequestSourceDto(e.Id, e.Name, e.IsActive, e.CreatedAt, e.UpdatedAt)).ToList();
    }
}
