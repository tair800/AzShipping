using MediatR;
using Request.Application.DTOs.Request;
using Request.Application.Features.Requests;
using Request.Domain.AggregatesModel.RequestAggregate;

namespace Request.Application.Features.Requests.Commands.CreateRequestType;

public sealed class CreateRequestTypeCommandHandler(IRequestTypeRepository repository)
    : IRequestHandler<CreateRequestTypeCommand, RequestTypeDto>
{
    public async Task<RequestTypeDto> Handle(CreateRequestTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = new RequestType
        {
            Id = Guid.NewGuid(),
            Code = request.Dto.Code,
            Name = request.Dto.Name,
            Direction = request.Dto.Direction,
            Mode = request.Dto.Mode,
            SubType = request.Dto.SubType,
            RequestNumberPrefix = request.Dto.RequestNumberPrefix,
            CarrierApiPath = request.Dto.CarrierApiPath,
            CarrierLabel = request.Dto.CarrierLabel,
            SortOrder = request.Dto.SortOrder,
            IsActive = true
        };
        var created = await repository.AddAsync(entity, cancellationToken);
        return RequestMapper.MapTypeToDto(created);
    }
}
