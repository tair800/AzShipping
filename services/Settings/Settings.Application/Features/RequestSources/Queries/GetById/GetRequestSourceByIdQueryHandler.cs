using MediatR;
using Settings.Application.DTOs.RequestSource;
using Settings.Domain.AggregatesModel.RequestSourceAggregate;

namespace Settings.Application.Features.RequestSources.Queries.GetById;

public sealed class GetRequestSourceByIdQueryHandler(IRequestSourceRepository repository) : IRequestHandler<GetRequestSourceByIdQuery, RequestSourceDto?>
{
    public async Task<RequestSourceDto?> Handle(GetRequestSourceByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await repository.GetByIdAsync(request.Id, cancellationToken);
        return e == null ? null : new RequestSourceDto(e.Id, e.Name, e.IsActive, e.CreatedAt, e.UpdatedAt);
    }
}
