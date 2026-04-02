using MediatR;
using Settings.Application.DTOs.RequestSource;
using Settings.Domain.AggregatesModel.RequestSourceAggregate;

namespace Settings.Application.Features.RequestSources.Commands.Update;

public sealed class UpdateRequestSourceCommandHandler(IRequestSourceRepository repository) : IRequestHandler<UpdateRequestSourceCommand, RequestSourceDto?>
{
    public async Task<RequestSourceDto?> Handle(UpdateRequestSourceCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        entity.Name = request.Dto.Name;
        entity.IsActive = request.Dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, cancellationToken);
        return new RequestSourceDto(entity.Id, entity.Name, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}
