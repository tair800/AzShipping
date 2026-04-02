using Accounting.Application.DTOs.OperationAct;
using Accounting.Domain.AggregatesModel.OperationActAggregate;
using MediatR;

namespace Accounting.Application.Features.OperationActs.Commands.Create;

public sealed class CreateOperationActCommandHandler(IOperationActRepository repo)
    : IRequestHandler<CreateOperationActCommand, OperationActListItemDto>
{
    public async Task<OperationActListItemDto> Handle(CreateOperationActCommand request, CancellationToken cancellationToken)
    {
        var list = await repo.GetAllAsync(cancellationToken);
        var nextOrder = list.Count == 0 ? 0 : list.Max(a => a.SortOrder) + 1;
        var entity = OperationActMapper.FromCreateDto(request.Dto, nextOrder);
        await repo.AddAsync(entity, cancellationToken);
        return OperationActMapper.ToListItemDto(entity);
    }
}
