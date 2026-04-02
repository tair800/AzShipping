using Clients.Application.DTOs.Direction;
using Clients.Application.Services;
using Clients.Domain.AggregatesModel.DirectionAggregate;
using MediatR;

namespace Clients.Application.Features.Directions.Commands.Update;

public sealed class UpdateDirectionCommandHandler(IDirectionRepository repository, IActionLogClient actionLogClient) : IRequestHandler<UpdateDirectionCommand, DirectionDto?>
{
    public async Task<DirectionDto?> Handle(UpdateDirectionCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;

        var dto = request.Dto;
        entity.FromCountryId = dto.FromCountryId;
        entity.FromCityId = dto.FromCityId;
        entity.ToCountryId = dto.ToCountryId;
        entity.ToCityId = dto.ToCityId;
        entity.Note = dto.Note;
        entity.Comments = dto.Comments;

        await repository.UpdateAsync(entity, cancellationToken);
        await actionLogClient.LogAsync("Client direction updated", $"direction: client {entity.ClientId} • id: {entity.Id}", null, null, cancellationToken);
        return new DirectionDto
        {
            Id = entity.Id,
            ClientId = entity.ClientId,
            FromCountryId = entity.FromCountryId,
            FromCityId = entity.FromCityId,
            ToCountryId = entity.ToCountryId,
            ToCityId = entity.ToCityId,
            Note = entity.Note,
            Comments = entity.Comments
        };
    }
}
