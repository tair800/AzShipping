using MediatR;
using Request.Application.DTOs.RequestNegotiation;
using Request.Application.Features.RequestNegotiations;
using Request.Domain.AggregatesModel.RequestNegotiationAggregate;

namespace Request.Application.Features.RequestNegotiations.Commands.Update;

public sealed class UpdateRequestNegotiationCommandHandler(IRequestNegotiationRepository repository) : IRequestHandler<UpdateRequestNegotiationCommand, RequestNegotiationDto?>
{
    public async Task<RequestNegotiationDto?> Handle(UpdateRequestNegotiationCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        var d = request.Dto;
        if (d.ClientId.HasValue) entity.ClientId = d.ClientId.Value;
        entity.ClientName = d.ClientName ?? entity.ClientName;
        entity.WayOfNegotiationId = d.WayOfNegotiationId ?? entity.WayOfNegotiationId;
        entity.WayOfNegotiationName = d.WayOfNegotiationName ?? entity.WayOfNegotiationName;
        entity.Question = d.Question ?? entity.Question;
        entity.Answer = d.Answer ?? entity.Answer;
        entity.Result = d.Result ?? entity.Result;
        await repository.UpdateAsync(entity, cancellationToken);
        var loaded = await repository.GetByIdAsync(entity.Id, cancellationToken);
        return RequestNegotiationMapper.MapToDto(loaded ?? entity);
    }
}
