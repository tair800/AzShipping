using Clients.Application.DTOs.Direction;
using Clients.Application.Services;
using Clients.Domain.AggregatesModel.DirectionAggregate;
using MediatR;

namespace Clients.Application.Features.Directions.Commands.Create;

public sealed class CreateDirectionCommandHandler(IDirectionRepository repository, IActionLogClient actionLogClient) : IRequestHandler<CreateDirectionCommand, DirectionDto>
{
    public async Task<DirectionDto> Handle(CreateDirectionCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var entity = new Direction
        {
            Id = Guid.NewGuid(),
            ClientId = dto.ClientId,
            FromCountryId = dto.FromCountryId,
            FromCityId = dto.FromCityId,
            ToCountryId = dto.ToCountryId,
            ToCityId = dto.ToCityId,
            Note = dto.Note,
            Comments = dto.Comments
        };
        await repository.AddAsync(entity, cancellationToken);
        var result = MapToDto(entity);
        await actionLogClient.LogAsync("Client direction created", $"direction: client {entity.ClientId} • id: {entity.Id}", null, null, cancellationToken);
        return result;
    }

    private static DirectionDto MapToDto(Direction e) => new()
    {
        Id = e.Id,
        ClientId = e.ClientId,
        FromCountryId = e.FromCountryId,
        FromCityId = e.FromCityId,
        ToCountryId = e.ToCountryId,
        ToCityId = e.ToCityId,
        Note = e.Note,
        Comments = e.Comments
    };
}
