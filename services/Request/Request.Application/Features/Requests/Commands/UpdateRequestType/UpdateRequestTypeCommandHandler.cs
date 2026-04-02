using MediatR;
using Request.Application.DTOs.Request;
using Request.Application.Features.Requests;
using Request.Domain.AggregatesModel.RequestAggregate;

namespace Request.Application.Features.Requests.Commands.UpdateRequestType;

public sealed class UpdateRequestTypeCommandHandler(IRequestTypeRepository repository)
    : IRequestHandler<UpdateRequestTypeCommand, RequestTypeDto?>
{
    public async Task<RequestTypeDto?> Handle(UpdateRequestTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;

        entity.Code = request.Dto.Code;
        entity.Name = request.Dto.Name;
        entity.Direction = request.Dto.Direction;
        entity.Mode = request.Dto.Mode;
        entity.SubType = request.Dto.SubType;
        entity.RequestNumberPrefix = request.Dto.RequestNumberPrefix;
        entity.CarrierApiPath = request.Dto.CarrierApiPath;
        entity.CarrierLabel = request.Dto.CarrierLabel;
        entity.SortOrder = request.Dto.SortOrder;
        entity.IsActive = request.Dto.IsActive;

        await repository.UpdateAsync(entity, cancellationToken);
        return RequestMapper.MapTypeToDto(entity);
    }
}
