using MediatR;
using Request.Application.DTOs.RequestNegotiation;
using Request.Application.Features.RequestNegotiations;
using Request.Domain.AggregatesModel.RequestNegotiationAggregate;

namespace Request.Application.Features.RequestNegotiations.Commands.Create;

public sealed class CreateRequestNegotiationCommandHandler(IRequestNegotiationRepository repository) : IRequestHandler<CreateRequestNegotiationCommand, RequestNegotiationDto>
{
    public async Task<RequestNegotiationDto> Handle(CreateRequestNegotiationCommand request, CancellationToken cancellationToken)
    {
        var d = request.Dto;
        var entity = new RequestNegotiation
        {
            Id = Guid.NewGuid(),
            ClientId = d.ClientId,
            ClientName = d.ClientName,
            WayOfNegotiationId = d.WayOfNegotiationId,
            WayOfNegotiationName = d.WayOfNegotiationName,
            CreationDate = DateTime.UtcNow,
            Question = d.Question,
            Answer = d.Answer,
            Result = d.Result
        };
        await repository.AddAsync(entity, cancellationToken);
        var loaded = await repository.GetByIdAsync(entity.Id, cancellationToken);
        return RequestNegotiationMapper.MapToDto(loaded ?? entity);
    }
}
