using MediatR;
using Settings.Application.DTOs.RequestSource;
using Settings.Domain.AggregatesModel.RequestSourceAggregate;

namespace Settings.Application.Features.RequestSources.Commands.Create;

public sealed class CreateRequestSourceCommandHandler(IRequestSourceRepository repository) : IRequestHandler<CreateRequestSourceCommand, RequestSourceDto>
{
    public async Task<RequestSourceDto> Handle(CreateRequestSourceCommand request, CancellationToken cancellationToken)
    {
        var entity = new RequestSource { Id = Guid.NewGuid(), Name = request.Dto.Name, IsActive = request.Dto.IsActive, CreatedAt = DateTime.UtcNow };
        await repository.AddAsync(entity, cancellationToken);
        return new RequestSourceDto(entity.Id, entity.Name, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}
